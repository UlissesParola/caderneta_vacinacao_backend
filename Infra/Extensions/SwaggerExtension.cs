using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Extensions;

public static class SwaggerExtension
{
    public static void AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.SwaggerDocument(o =>
        {
            o.DocumentSettings = s =>
            {
                s.Title = "API Caderneta de Vacinação";
                s.Version = "v1";
                s.Description = "API para gestão de cadernetas de vacinação";
            };
        });
    }

    public static void UseSwaggerDocumentation(this WebApplication app)
    {
        app.UseOpenApi();
        app.UseSwaggerGen();

        // Configura a interface do Swagger UI
        app.UseSwaggerUi(settings =>
        {
            settings.Path = "/swagger";
            settings.DocumentPath = "/swagger/v1/swagger.json";
            settings.DocumentTitle = "Documentação da API - Caderneta de Vacinação";
        });
    }
}
