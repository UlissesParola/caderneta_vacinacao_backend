using FastEndpoints;
using FastEndpoints.Swagger;
using Infra.Data.Context;
using Infra.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug() // Definir o nível mínimo de log
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

// Usar o Serilog como o logger da aplicação
builder.Host.UseSerilog(Log.Logger);

builder.Services.AddSwaggerDocumentation();
builder.Services.AddFastEndpoints();
builder.Services.AddIoC();
builder.Services.AddIdentityConfiguration();
builder.Services.AddDatabaseConfiguration(builder.Configuration, builder.Environment);

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
    serverOptions.ListenAnyIP(int.Parse(port));
});

DapperExtension.AddDapperTypeHandlers();

var app = builder.Build();

app.MigrateDatabase<AppDbContext>();

app.UseSwaggerDocumentation();
app.UseFastEndpoints();

try
{
    app.Run();
}
catch (Exception ex)
{
    // Registrar erros de inicialização
    Log.Fatal(ex, "Aplicação falhou ao iniciar");
}
finally
{
    Log.CloseAndFlush(); // Garantir que todos os logs sejam gravados antes de encerrar
}

public partial class Program { }


