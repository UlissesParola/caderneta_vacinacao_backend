using Core.Entities;
using Infra.Data.Repositories.Commands;
using Infra.Data.Repositories.Queries;

namespace Infra.Tests.Data.Repositories.Queries;

public class UsuarioQueryRepositoryIntegrationTests : IntegrationTestBase
{
    private readonly UsuarioCommandRepository _usuarioCommandRepository;

    public UsuarioQueryRepositoryIntegrationTests()
    {
        // Inicializando o repositório de comandos para criar usuários
        _usuarioCommandRepository = new UsuarioCommandRepository(_userManager, _context);
    }

    [Fact]
    public async Task GetUsuarioByEmailAsync_ShouldReturnUsuario_WhenUserExists()
    {
        // Arrange
        var repository = new UsuarioQueryRepository(_dbConnection, _userManager, _signInManager);

        // Criando um Usuario usando o repositório de comandos
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

        // Usando o método CreateUsuarioAsync do repositório para criar o usuário
        var createResult = await _usuarioCommandRepository.CreateUsuarioAsync(usuario, password);
        Assert.True(createResult);

        // Act
        var result = await repository.GetUsuarioByEmailAsync("joao.silva@example.com");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(usuario.Id, result.Id);
        Assert.Equal(usuario.Email, result.Email);
    }

    [Fact]
    public async Task ValidateUsuarioCredentialsAsync_ShouldReturnTrue_WhenCredentialsAreValid()
    {
        // Arrange
        var repository = new UsuarioQueryRepository(_dbConnection, _userManager, _signInManager);

        // Criando um Usuario usando o repositório de comandos
        var usuario = new Usuario
        {
            Id = "2",
            Nome = "Maria",
            Sobrenome = "Santos",
            Cpf = "98765432100",
            Email = "maria.santos@example.com",
            DataNascimento = new DateOnly(1985, 3, 20),
            Sexo = "Feminino",
            UsuarioDependente = new List<UsuarioDependente>()
        };
        string password = "SenhaSegura123";

        // Usando o método CreateUsuarioAsync do repositório para criar o usuário
        var createResult = await _usuarioCommandRepository.CreateUsuarioAsync(usuario, password);
        Assert.True(createResult);

        // Act
        var isValid = await repository.ValidateUsuarioCredentialsAsync("maria.santos@example.com", "SenhaSegura123");

        // Assert
        Assert.True(isValid);
    }
}