using Core.Entities;
using Infra.Data.Context;
using Infra.Data.Repositories.Commands;
using Infra.Data.Repositories.Queries;
using Infra.Extensions;
using Infra.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Data;
using System.Text.Encodings.Web;

namespace Infra.Tests.Data.Repositories.Queries;

public class UsuarioQueryRepositoryIntegrationTests : IAsyncLifetime
{
    private readonly SqliteConnection _connection;
    private readonly AppDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IDbConnection _dbConnection;
    private readonly UsuarioCommandRepository _usuarioCommandRepository;

    public UsuarioQueryRepositoryIntegrationTests()
    {
        // Criação da conexão SQLite em memória
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open(); // Manter a conexão aberta

        // Configuração do DbContext usando a mesma conexão aberta
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(_connection)
            .Options;

        // Configuração dos serviços de identidade e autenticação
        var services = new ServiceCollection();
        services.AddSingleton(new IdentityOptions());
        services.AddLogging();
        services.AddDataProtection();
        services.AddIdentityConfiguration();

        // Adicionando serviços de autenticação necessários para o SignInManager
        services.AddAuthentication("Test")
                .AddScheme<AuthenticationSchemeOptions, TestAuthenticationHandler>("Test", options => { });

        // Configuração do DbContext e Identity
        services.AddDbContext<AppDbContext>(opt => opt.UseSqlite(_connection));
        services.AddIdentityCore<ApplicationUser>(options => { })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        var serviceProvider = services.BuildServiceProvider();
        _context = serviceProvider.GetRequiredService<AppDbContext>();
        _userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        _signInManager = serviceProvider.GetRequiredService<SignInManager<ApplicationUser>>();

        // Criação do banco de dados na memória
        _context.Database.EnsureCreated();

        // Obtendo a conexão do IDbConnection a partir do AppDbContext
        _dbConnection = _context.Database.GetDbConnection();

        // Inicializando o repositório de comandos para criar usuários
        _usuarioCommandRepository = new UsuarioCommandRepository(_userManager, _context);
    }

    // Limpeza após execução dos testes
    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync()
    {
        await _connection.CloseAsync();
    }

    [Fact]
    public async Task GetUsuarioByEmailAsync_ShouldReturnUsuario_WhenUserExists()
    {
        // Arrange
        var repository = new UsuarioQueryRepository(_dbConnection, _userManager, _signInManager);

        // Criando um Usuario usando o repositório de comandos
        var usuario = new Usuario
        {
            Id = "1",
            Nome = "João",
            Sobrenome = "Silva",
            Cpf = "12345678901",
            Email = "joao.silva@example.com",
            DataNascimento = new DateOnly(1990, 5, 15),
            Sexo = "Masculino",
            UsuarioDependente = new List<UsuarioDependente>()
        };
        string password = "SenhaSegura123";

        // Usando o método CreateUsuarioAsync do repositório para criar o usuário
        var createResult = await _usuarioCommandRepository.CreateUsuarioAsync(usuario, password);
        Assert.True(createResult);

        // Act
        var result = await repository.GetUsuarioByEmailAsync("joao.silva@example.com");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(usuario.Id, result.Id);
        Assert.Equal(usuario.Email, result.Email);
    }

    [Fact]
    public async Task ValidateUsuarioCredentialsAsync_ShouldReturnTrue_WhenCredentialsAreValid()
    {
        // Arrange
        var repository = new UsuarioQueryRepository(_dbConnection, _userManager, _signInManager);

        // Criando um Usuario usando o repositório de comandos
        var usuario = new Usuario
        {
            Id = "2",
            Nome = "Maria",
            Sobrenome = "Santos",
            Cpf = "98765432100",
            Email = "maria.santos@example.com",
            DataNascimento = new DateOnly(1985, 3, 20),
            Sexo = "Feminino",
            UsuarioDependente = new List<UsuarioDependente>()
        };
        string password = "SenhaSegura123";

        // Usando o método CreateUsuarioAsync do repositório para criar o usuário
        var createResult = await _usuarioCommandRepository.CreateUsuarioAsync(usuario, password);
        Assert.True(createResult);

        // Act
        var isValid = await repository.ValidateUsuarioCredentialsAsync("maria.santos@example.com", "SenhaSegura123");

        // Assert
        Assert.True(isValid);
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