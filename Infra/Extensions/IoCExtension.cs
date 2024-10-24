using Core.Interfaces.InfraServices;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Services;
using Infra.InfraServices;
using Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Extensions;

public static class IoCExtension
{
    public static void AddIoC(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();     // Repositório de Usuários
        services.AddScoped<IIdentityService, IdentityService>();   // Serviço de Identidade
        services.AddScoped<IAuthService, AuthService>();           // Serviço de Autenticação
    }
}
