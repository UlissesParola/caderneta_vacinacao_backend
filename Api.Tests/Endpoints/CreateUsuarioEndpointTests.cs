using Api.Requests;
using Core.Interfaces.Services;
using Core.Utils;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http.Json;
using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Core.Dto;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Tests.Endpoints;

public class CreateUsuarioEndpointTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly Mock<IUsuarioService> _usuarioServiceMock;

    public CreateUsuarioEndpointTests(CustomWebApplicationFactory factory)
    {
        // Criando o cliente HTTP para os testes
        _client = factory.CreateClient();

        // Inicializando o mock para controle nos testes
        _usuarioServiceMock = Mock.Get(factory.Services.GetRequiredService<IUsuarioService>());
    }

    [Fact]
    public async Task CreateUsuario_ShouldReturnOk_WhenUserIsCreatedSuccessfully()
    {
        // Arrange
        var request = new CreateUsuarioRequest(
            Nome: "João",
            Sobrenome: "Silva",
            Cpf: "12345678901",
            Email: "joao.silva@example.com",
            Password: "SenhaSegura123",
            DataNascimento: new DateOnly(1990, 5, 15),
            Sexo: "Masculino"
        );

        // Configurando o mock para retornar sucesso
        _usuarioServiceMock
            .Setup(service => service.CreateUsuarioAsync(It.IsAny<UsuarioDTO>()))
            .ReturnsAsync(Result<bool>.Success(true));

        // Act
        var response = await _client.PostAsJsonAsync("/api/usuarios", request);

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
        Assert.NotNull(content);
        Assert.True(content.ContainsKey("message"));
        Assert.Equal("Usuário criado com sucesso!", content["message"]);
    }

    [Fact]
    public async Task CreateUsuario_ShouldReturnBadRequest_WhenUserCreationFails()
    {
        // Arrange
        var request = new CreateUsuarioRequest(
            Nome: "Ana",
            Sobrenome: "Costa",
            Cpf: "98765432100",
            Email: "ana.costa@example.com",
            Password: "SenhaInvalida",
            DataNascimento: new DateOnly(1985, 3, 20),
            Sexo: "Feminino"
        );

        // Configurando o mock para retornar uma falha
        _usuarioServiceMock
            .Setup(service => service.CreateUsuarioAsync(It.IsAny<UsuarioDTO>()))
            .ReturnsAsync(Result<bool>.Failure("Erro ao criar o usuário.", 400));

        // Act
        var response = await _client.PostAsJsonAsync("/api/usuarios", request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var content = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
        Assert.NotNull(content);
        Assert.True(content.ContainsKey("message"));
        Assert.Equal("Erro ao criar o usuário.", content["message"]);
    }
}