using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Extensions;

public static class CorsExtension
{
    public static void AddCors(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", policy =>
            {
                policy.WithOrigins(
                    "https://caderneta-vacinacao-693e63316fd9.herokuapp.com", // Domínio do frontend em produção
                    "http://localhost:4200",                    // Domínio do frontend em desenvolvimento
                    "http://localhost:5000"
                )
                .AllowAnyHeader()
                .AllowAnyMethod();
            });
        });
    }
}