using Core.Entities;
using Infra.Data.Context;
using Infra.Data.Repositories.Commands;
using Infra.Extensions;
using Infra.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Data;

namespace Infra.Tests.Data.Repositories.Commands;

public class UsuarioCommandRepositoryIntegrationTests : IntegrationTestBase
{
    private readonly UsuarioCommandRepository _repository;

    public UsuarioCommandRepositoryIntegrationTests()
    {
        // Inicializando o repositório de comandos usando a configuração da classe base
        _repository = new UsuarioCommandRepository(_userManager, _context);
    }

    [Fact]
    public async Task CreateUsuarioAsync_ShouldReturnTrue_WhenUserIsCreatedSuccessfully()
    {
        // Arrange
        var usuario = new Usuario
        {
            Id = "1",
            Nome = "João",
            Sobrenome = "Silva",
            Cpf = "12345678901",
            Email = "joao.silva@example.com",
            DataNascimento = new DateOnly(1990, 5, 15),
            Sexo = "Masculino",
            UsuarioDependente = new List<UsuarioDependente>()
        };
        string password = "SenhaSegura123";

        // Act
        var result = await _repository.CreateUsuarioAsync(usuario, password);

        // Assert
        Assert.True(result);
        var createdUsuario = await _context.Set<Usuario>().FindAsync("1");
        Assert.NotNull(createdUsuario);
        Assert.Equal("João", createdUsuario.Nome);

        // Verificar se o ApplicationUser foi criado corretamente
        var createdAppUser = await _userManager.FindByIdAsync(createdUsuario.ApplicationUserId);
        Assert.NotNull(createdAppUser);
        Assert.Equal("joao.silva@example.com", createdAppUser.Email);
    }

    [Fact]
    public async Task CreateUsuarioAsync_ShouldReturnFalse_WhenUserCreationFails()
    {
        // Arrange
        var usuario = new Usuario
        {
            Id = "2",
            Nome = "Ana",
            Sobrenome = "Costa",
            Cpf = "98765432100",
            Email = "ana.costa@example.com",
            DataNascimento = new DateOnly(1985, 3, 20),
            Sexo = "Feminino",
            UsuarioDependente = new List<UsuarioDependente>()
        };
        string password = ""; // Password vazio para forçar falha

        // Act
        var result = await _repository.CreateUsuarioAsync(usuario, password);

        // Assert
        Assert.False(result);
        var createdUsuario = await _context.Set<Usuario>().FindAsync("2");
        Assert.Null(createdUsuario);
    }

    [Fact]
    public async Task UpdateUsuarioAsync_ShouldReturnTrue_WhenUserIsUpdatedSuccessfully()
    {
        // Arrange
        var usuario = new Usuario
        {
            Id = "3",
            Nome = "Carlos",
            Sobrenome = "Oliveira",
            Cpf = "32165498701",
            Email = "carlos.oliveira@example.com",
            DataNascimento = new DateOnly(1975, 7, 10),
            Sexo = "Masculino",
            UsuarioDependente = new List<UsuarioDependente>()
        };
        string password = "SenhaSegura123";

        // Crie o usuário usando o método do repositório
        await _repository.CreateUsuarioAsync(usuario, password);

        // Atualiza os dados do usuário
        usuario.Nome = "Carlos Atualizado";
        var result = await _repository.UpdateUsuarioAsync(usuario);

        // Assert
        Assert.True(result);
        var updatedUsuario = await _context.Set<Usuario>().FindAsync("3");
        Assert.Equal("Carlos Atualizado", updatedUsuario.Nome);
    }

    [Fact]
    public async Task DeleteUsuarioAsync_ShouldReturnTrue_WhenUserIsDeletedSuccessfully()
    {
        // Arrange
        var usuario = new Usuario
        {
            Id = "4",
            Nome = "Mariana",
            Sobrenome = "Santos",
            Cpf = "85296374100",
            Email = "mariana.santos@example.com",
            DataNascimento = new DateOnly(1992, 12, 5),
            Sexo = "Feminino",
            UsuarioDependente = new List<UsuarioDependente>()
        };
        string password = "SenhaSegura123";

        // Crie o usuário usando o método do repositório
        await _repository.CreateUsuarioAsync(usuario, password);

        // Act
        var result = await _repository.DeleteUsuarioAsync("4");

        // Assert
        Assert.True(result);
        var deletedUsuario = await _context.Set<Usuario>().FindAsync("4");
        Assert.Null(deletedUsuario);
    }

    [Fact]
    public async Task ChangePasswordAsync_ShouldReturnTrue_WhenPasswordIsChangedSuccessfully()
    {
        // Arrange
        var usuario = new Usuario
        {
            Id = "5",
            Nome = "Paulo",
            Sobrenome = "Gomes",
            Cpf = "45678912345",
            Email = "paulo.gomes@example.com",
            DataNascimento = new DateOnly(1980, 11, 8),
            Sexo = "Masculino",
            UsuarioDependente = new List<UsuarioDependente>()
        };
        string password = "SenhaSegura123";

        // Crie o usuário usando o método do repositório
        await _repository.CreateUsuarioAsync(usuario, password);

        // Change the password
        var result = await _repository.ChangePasswordAsync("5", "NovaSenha123");

        // Assert
        Assert.True(result);
    }
}