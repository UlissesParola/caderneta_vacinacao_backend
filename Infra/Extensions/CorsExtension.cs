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
                policy.AllowAnyOrigin()   // Permite qualquer origem
                      .AllowAnyHeader()   // Permite todos os cabeçalhos
                      .AllowAnyMethod()
                      .AllowCredentials();
            });
        });
    }
}