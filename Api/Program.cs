using FastEndpoints;
using FastEndpoints.Swagger;
using Infra.Data.Context;
using Infra.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug() // Definir o n�vel m�nimo de log
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

// Usar o Serilog como o logger da aplica��o
builder.Host.UseSerilog(Log.Logger);

builder.Services.AddSwaggerDocumentation();
builder.Services.AddFastEndpoints();
builder.Services.AddIoC();
builder.Services.AddIdentityConfiguration();
builder.Services.AddDatabaseConfiguration(builder.Configuration, builder.Environment);
builder.Services.AddCors();

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
    serverOptions.ListenAnyIP(int.Parse(port));
});

DapperExtension.AddDapperTypeHandlers();

var app = builder.Build();

app.UseCors("AllowHerokuClient");

app.Use(async (context, next) =>
{
    if (context.Request.Method == "OPTIONS")
    {
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*"); // Ou especifique a origem permitida
        context.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
        context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization");

        context.Response.StatusCode = StatusCodes.Status204NoContent; // Responde com 204 (sem conte�do) para `OPTIONS`
        return;
    }

    await next();
});

app.MigrateDatabase<AppDbContext>();

app.UseSwaggerDocumentation();
app.UseFastEndpoints();

try
{
    app.Run();
}
catch (Exception ex)
{
    // Registrar erros de inicializa��o
    Log.Fatal(ex, "Aplica��o falhou ao iniciar");
}
finally
{
    Log.CloseAndFlush(); // Garantir que todos os logs sejam gravados antes de encerrar
}

public partial class Program { }


