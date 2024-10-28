using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Npgsql;

namespace Infra.InfraServices;

public static class ConnectionStringProvider
{
    public static string GetConnectionString(IConfiguration configuration, IHostEnvironment environment)
    {
        if (environment.IsProduction())
        {
            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            if (!string.IsNullOrEmpty(databaseUrl))
            {
                var databaseUri = new Uri(databaseUrl);
                var userInfo = databaseUri.UserInfo.Split(':');
                var connectionStringBuilder = new NpgsqlConnectionStringBuilder
                {
                    Host = databaseUri.Host,
                    Port = databaseUri.Port,
                    Username = userInfo[0],
                    Password = userInfo[1],
                    Database = databaseUri.LocalPath.TrimStart('/'),
                    SslMode = SslMode.Require,
                };

                return connectionStringBuilder.ToString();
            }
            else
            {
                throw new InvalidOperationException("A variável de ambiente DATABASE_URL não foi encontrada no ambiente de produção.");
            }
        }
        else
        {
            return configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("A ConnectionString não foi encontrada no ambiente de desenvolvimento."); ;
        }
    }
}