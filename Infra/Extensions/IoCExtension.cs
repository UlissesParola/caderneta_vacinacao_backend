using Core.Interfaces.InfraServices;
using Core.Interfaces.Repositories;
using Core.Interfaces.Repositories.Commands;
using Core.Interfaces.Repositories.Queries;
using Core.Interfaces.Services;
using Core.Services;
using Infra.Data.Repositories;
using Infra.Data.Repositories.Commands;
using Infra.Data.Repositories.Queries;
using Infra.InfraServices;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Extensions;

public static class IoCExtension
{
    public static void AddIoC(this IServiceCollection services)
    {
        //Repositories
        services.AddScoped<IUsuarioCommandRepository, UsuarioCommandRepository>();
        services.AddScoped<IUsuarioQueryRepository, UsuarioQueryRepository>();
        services.AddScoped<IDependenteCommandRepository, DependenteCommandRepository>();
        services.AddScoped<IDependenteQueryRepository, DependenteQueryRepository>();

        services.AddScoped<IVacinaCommandRepository, VacinaCommandRepository>();
        services.AddScoped<IVacinaQueryRepository, VacinaQueryRepository>();

        services.AddScoped<IRegistroVacinaCommandRepository, RegistroVacinaCommandRepository>();
        services.AddScoped<IRegistroVacinaQueryRepository, RegistroVacinaQueryRepository>();

        //Services
        services.AddScoped<IIdentityService, IdentityService>();  
        services.AddScoped<IAuthService, AuthService>();    
        services.AddScoped<IUsuarioService, UsuarioService>();
    }
}
