using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System.Data;

namespace Infra.Extensions;

public static class DatabaseExtension
{
    public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        //EF
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));

        //Dapper
        services.AddScoped<IDbConnection>(sp =>
            new NpgsqlConnection(connectionString));

        return services;
    }
}