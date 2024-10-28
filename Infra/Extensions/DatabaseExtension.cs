using Infra.Data.Context;
using Infra.InfraServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using System.Data;

namespace Infra.Extensions;

public static class DatabaseExtension
{
    public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
    {
        var connectionString = ConnectionStringProvider.GetConnectionString(configuration, env);

        // EF Core - Configuração do banco de dados
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));

        // Dapper - Configuração da conexão
        services.AddScoped<IDbConnection>(sp =>
            new NpgsqlConnection(connectionString));

        return services;
    }

    // Método de extensão para aplicar migrações
    public static void MigrateDatabase<TContext>(this WebApplication app) where TContext : DbContext
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<TContext>();
                context.Database.Migrate(); // Aplica as migrações
            }
            catch (Exception ex)
            {
                // Adicione log ou tratamento de exceção se necessário
                Console.WriteLine($"Erro ao aplicar migrações: {ex.Message}");
                throw;
            }
        }
    }
}