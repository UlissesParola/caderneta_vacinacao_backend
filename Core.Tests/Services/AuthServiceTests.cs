using Core.Entities;
using Core.Interfaces.InfraServices;
using Core.Interfaces.Repositories.Queries;
using Core.Services;
using Moq;

namespace Core.Tests.Services;

public class AuthServiceTests
{
    private readonly Mock<IIdentityService> _identityServiceMock;
    private readonly Mock<IUsuarioQueryRepository> _usuarioQueryRepositoryMock;
    private readonly AuthService _authService;

    public AuthServiceTests()
    {
        // Inicializa mocks
        _identityServiceMock = new Mock<IIdentityService>();
        _usuarioQueryRepositoryMock = new Mock<IUsuarioQueryRepository>();

        // Instancia o serviço com os mocks
        _authService = new AuthService(_identityServiceMock.Object, _usuarioQueryRepositoryMock.Object);
    }

    [Fact]
    public async Task ValidateUserCredentialsAsync_ShouldReturnTrue_WhenCredentialsAreValid()
    {
        // Arrange
        string email = "test@example.com";
        string password = "validPassword";

        // Configura o mock para retornar true quando as credenciais forem válidas
        _identityServiceMock
            .Setup(service => service.ValidateUserCredentialsAsync(email, password))
            .ReturnsAsync(true);

        // Act
        var result = await _authService.ValidateUserCredentialsAsync(email, password);

        // Assert
        Assert.True(result);
        _identityServiceMock.Verify(service => service.ValidateUserCredentialsAsync(email, password), Times.Once);
    }

    [Fact]
    public async Task ValidateUserCredentialsAsync_ShouldReturnFalse_WhenCredentialsAreInvalid()
    {
        // Arrange
        string email = "test@example.com";
        string password = "invalidPassword";

        // Configura o mock para retornar false quando as credenciais forem inválidas
        _identityServiceMock
            .Setup(service => service.ValidateUserCredentialsAsync(email, password))
            .ReturnsAsync(false);

        // Act
        var result = await _authService.ValidateUserCredentialsAsync(email, password);

        // Assert
        Assert.False(result);
        _identityServiceMock.Verify(service => service.ValidateUserCredentialsAsync(email, password), Times.Once);
    }

    [Fact]
    public async Task GetUserByEmailAsync_ShouldReturnUsuario_WhenUserExists()
    {
        // Arrange
        string email = "test@example.com";
        var expectedUser = new Usuario
        {
            Id = "123",
            Nome = "João",
            Sobrenome = "Silva",
            Email = email
        };

        // Configura o mock para retornar um usuário quando o email for encontrado
        _usuarioQueryRepositoryMock
            .Setup(repo => repo.GetUsuarioByEmailAsync(email))
            .ReturnsAsync(expectedUser);

        // Act
        var result = await _authService.GetUserByEmailAsync(email);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedUser.Id, result.Id);
        Assert.Equal(expectedUser.Nome, result.Nome);
        Assert.Equal(expectedUser.Email, result.Email);
        _usuarioQueryRepositoryMock.Verify(repo => repo.GetUsuarioByEmailAsync(email), Times.Once);
    }

    [Fact]
    public async Task GetUserByEmailAsync_ShouldReturnNull_WhenUserDoesNotExist()
    {
        // Arrange
        string email = "nonexistent@example.com";

        // Configura o mock para retornar null quando o email não for encontrado
        _usuarioQueryRepositoryMock
            .Setup(repo => repo.GetUsuarioByEmailAsync(email))
            .ReturnsAsync((Usuario?)null);

        // Act
        var result = await _authService.GetUserByEmailAsync(email);

        // Assert
        Assert.Null(result);
        _usuarioQueryRepositoryMock.Verify(repo => repo.GetUsuarioByEmailAsync(email), Times.Once);
    }
}