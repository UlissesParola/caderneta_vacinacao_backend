using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;

namespace Api.Tests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        // Configuração personalizada do HostBuilder para testes
        builder.ConfigureServices(services =>
        {
            // Criando o mock do IUsuarioService
            var usuarioServiceMock = new Mock<IUsuarioService>();

            // Configurando o mock para ser utilizado nos testes
            services.AddSingleton(usuarioServiceMock.Object);
        });

        return base.CreateHost(builder);
    }
}