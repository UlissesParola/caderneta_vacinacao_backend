using Infra.Data.Context;
using Infra.Data.DapperTypeHandlers;
using Infra.Extensions;
using Infra.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Data;
using System.Text.Encodings.Web;

namespace Infra.Tests;

public abstract class IntegrationTestBase : IAsyncLifetime
{
    protected readonly SqliteConnection _connection;
    protected readonly AppDbContext _context;
    protected readonly UserManager<ApplicationUser> _userManager;
    protected readonly SignInManager<ApplicationUser> _signInManager;
    protected readonly IDbConnection _dbConnection;
    protected readonly ServiceProvider _serviceProvider;

    protected IntegrationTestBase()
    {
        // Criação da conexão SQLite em memória
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open(); // Manter a conexão aberta

        // Configuração do DbContext usando a mesma conexão aberta
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(_connection)
            .Options;

        DapperExtension.AddDapperTypeHandlers();

        // Configuração do WebApplicationBuilder no .NET 8
        var builder = WebApplication.CreateBuilder(new WebApplicationOptions
        {
            EnvironmentName = Environments.Development
        });

        // Configuração dos serviços de identidade e autenticação
        builder.Services.AddSingleton(new IdentityOptions());
        builder.Services.AddLogging();
        builder.Services.AddDataProtection();
        builder.Services.AddIdentityConfiguration();

        // Adicionando suporte para IHttpContextAccessor
        builder.Services.AddHttpContextAccessor();

        // Configuração do DbContext e Identity
        builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlite(_connection));
        builder.Services.AddIdentityCore<ApplicationUser>(options => { })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        // Construção do WebApplication
        var app = builder.Build();

        // Configurando o middleware para configurar o HttpContext durante os testes
        app.Use(async (context, next) =>
        {
            var contextAccessor = context.RequestServices.GetRequiredService<IHttpContextAccessor>();
            contextAccessor.HttpContext = context; // Configura o HttpContext para o SignInManager
            await next(); // Continue para o próximo middleware
        });

        // Criando o TestServer com os serviços configurados
        var server = new TestServer(app.Services);

        // Criação do ServiceProvider e injeção de dependências
        _serviceProvider = builder.Services.BuildServiceProvider();
        _context = _serviceProvider.GetRequiredService<AppDbContext>();
        _userManager = _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        _signInManager = _serviceProvider.GetRequiredService<SignInManager<ApplicationUser>>();

        // Criação do banco de dados na memória
        _context.Database.EnsureCreated();

        // Obtendo a conexão do IDbConnection a partir do AppDbContext
        _dbConnection = _context.Database.GetDbConnection();
    }

    // Limpeza após execução dos testes
    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync()
    {
        await _connection.CloseAsync();
    }
}

// Implementação de um AuthenticationHandler para testes
public class TestAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public TestAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
        : base(options, logger, encoder, clock) { }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var claims = new[] { new System.Security.Claims.Claim("sub", "testuser") };
        var identity = new System.Security.Claims.ClaimsIdentity(claims, "test");
        var principal = new System.Security.Claims.ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, "test");

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}