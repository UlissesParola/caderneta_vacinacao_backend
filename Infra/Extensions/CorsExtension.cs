using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Extensions;

public static class CorsExtension
{
    public static void AddCors(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowHerokuClient",
                builder =>
                {
                    builder.WithOrigins("https://caderneta-vacinacao-693e63316fd9.herokuapp.com/")  // Origem do Angular
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
        });
    }
}