using Core.Dto;
using Core.Entities;
using Core.Interfaces.Repositories.Commands;
using Core.Services;
using Core.Utils;
using Moq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

namespace Core.Tests.Services;

public class UsuarioServiceTests
{
    private readonly Mock<IUsuarioCommandRepository> _usuarioCommandRepositoryMock;
    private readonly UsuarioService _usuarioService;

    public UsuarioServiceTests()
    {
        // Inicializa o mock do repositório de comando
        _usuarioCommandRepositoryMock = new Mock<IUsuarioCommandRepository>();

        // Cria uma instância do serviço usando o mock
        _usuarioService = new UsuarioService(_usuarioCommandRepositoryMock.Object);
    }

    [Fact]
    public async Task CreateUsuarioAsync_ShouldReturnSuccess_WhenRepositoryReturnsTrue()
    {
        // Arrange
        var usuarioDto = new UsuarioDTO
        {
            Nome = "João",
            Sobrenome = "Silva",
            Cpf = "12345678900",
            Email = "joao@example.com",
            Password = "senhaSegura",
            DataNascimento = new DateOnly(1990, 1, 1),
            Sexo = "M"
        };

        // Configura o mock para retornar true quando o método CreateUsuarioAsync for chamado
        _usuarioCommandRepositoryMock
            .Setup(repo => repo.CreateUsuarioAsync(It.IsAny<Usuario>(), usuarioDto.Password))
            .ReturnsAsync(Result<bool>.Success(true));

        // Act
        var result = await _usuarioService.CreateUsuarioAsync(usuarioDto);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(200, result.StatusCode);
        Assert.True(result.Value);
        Assert.Null(result.ErrorMessage);
        _usuarioCommandRepositoryMock.Verify(repo => repo.CreateUsuarioAsync(It.IsAny<Usuario>(), usuarioDto.Password), Times.Once);
    }

    [Fact]
    public async Task CreateUsuarioAsync_ShouldReturnFailure_WhenRepositoryReturnsFalse()
    {
        // Arrange
        var usuarioDto = new UsuarioDTO
        {
            Nome = "João",
            Sobrenome = "Silva",
            Cpf = "12345678900",
            Email = "joao@example.com",
            Password = "senhaSegura",
            DataNascimento = new DateOnly(1990, 1, 1),
            Sexo = "M"
        };

        // Configura o mock para retornar false quando o método CreateUsuarioAsync for chamado
        _usuarioCommandRepositoryMock
            .Setup(repo => repo.CreateUsuarioAsync(It.IsAny<Usuario>(), usuarioDto.Password))
            .ReturnsAsync(Result<bool>.Failure("Erro ao criar o usuário.", 400));

        // Act
        var result = await _usuarioService.CreateUsuarioAsync(usuarioDto);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(400, result.StatusCode);
        Assert.Equal("Erro ao criar o usuário.", result.ErrorMessage);
        _usuarioCommandRepositoryMock.Verify(repo => repo.CreateUsuarioAsync(It.IsAny<Usuario>(), usuarioDto.Password), Times.Once);
    }
}
