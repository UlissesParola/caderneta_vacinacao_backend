using Infra.Data.Context;
using Infra.InfraServices;
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

        //EF
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));

        //Dapper
        services.AddScoped<IDbConnection>(sp =>
            new NpgsqlConnection(connectionString));

        return services;
    }
}