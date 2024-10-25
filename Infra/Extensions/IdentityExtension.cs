using Infra.Data.Context;
using Infra.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Extensions;

public static class IdentityExtension
{
    public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            // Configurações de senha
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;

            // Configurações de bloqueio
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // Configurações de usuário
            options.User.RequireUniqueEmail = true;

            // Configurações de sign-in
            options.SignIn.RequireConfirmedEmail = false; // Altere para true se quiser confirmação por email
        })
            .AddEntityFrameworkStores<AppDbContext>() // Configura o Identity para usar o seu DbContext
            .AddDefaultTokenProviders(); // Adiciona provedores de token padrão (ex: recuperação de senha)

        return services;
    }
}