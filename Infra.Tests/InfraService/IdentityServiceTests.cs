using Infra.Identity;
using Infra.InfraServices;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Infra.Tests.InfraService;

public class IdentityServiceTests
{
    private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
    private readonly IdentityService _identityService;

    public IdentityServiceTests()
    {
        // Cria um mock do UserManager<ApplicationUser> usando seus requisitos de construção
        var store = new Mock<IUserStore<ApplicationUser>>();
        _userManagerMock = new Mock<UserManager<ApplicationUser>>(
            store.Object, null, null, null, null, null, null, null, null);

        // Instancia o serviço com o mock
        _identityService = new IdentityService(_userManagerMock.Object);
    }

    [Fact]
    public async Task ValidateUserCredentialsAsync_ShouldReturnTrue_WhenCredentialsAreValid()
    {
        // Arrange
        string email = "test@example.com";
        string password = "validPassword";

        var appUser = new ApplicationUser { Email = email };

        // Configura o mock para encontrar o usuário pelo email e validar a senha
        _userManagerMock
            .Setup(manager => manager.FindByEmailAsync(email))
            .ReturnsAsync(appUser);

        _userManagerMock
            .Setup(manager => manager.CheckPasswordAsync(appUser, password))
            .ReturnsAsync(true);

        // Act
        var result = await _identityService.ValidateUserCredentialsAsync(email, password);

        // Assert
        Assert.True(result);
        _userManagerMock.Verify(manager => manager.FindByEmailAsync(email), Times.Once);
        _userManagerMock.Verify(manager => manager.CheckPasswordAsync(appUser, password), Times.Once);
    }

    [Fact]
    public async Task ValidateUserCredentialsAsync_ShouldReturnFalse_WhenUserDoesNotExist()
    {
        // Arrange
        string email = "nonexistent@example.com";
        string password = "somePassword";

        // Configura o mock para retornar null quando o usuário não for encontrado
        _userManagerMock
            .Setup(manager => manager.FindByEmailAsync(email))
            .ReturnsAsync((ApplicationUser?)null);

        // Act
        var result = await _identityService.ValidateUserCredentialsAsync(email, password);

        // Assert
        Assert.False(result);
        _userManagerMock.Verify(manager => manager.FindByEmailAsync(email), Times.Once);
        _userManagerMock.Verify(manager => manager.CheckPasswordAsync(It.IsAny<ApplicationUser>(), password), Times.Never);
    }

    [Fact]
    public async Task ValidateUserCredentialsAsync_ShouldReturnFalse_WhenPasswordIsInvalid()
    {
        // Arrange
        string email = "test@example.com";
        string password = "invalidPassword";

        var appUser = new ApplicationUser { Email = email };

        // Configura o mock para encontrar o usuário, mas a senha ser inválida
        _userManagerMock
            .Setup(manager => manager.FindByEmailAsync(email))
            .ReturnsAsync(appUser);

        _userManagerMock
            .Setup(manager => manager.CheckPasswordAsync(appUser, password))
            .ReturnsAsync(false);

        // Act
        var result = await _identityService.ValidateUserCredentialsAsync(email, password);

        // Assert
        Assert.False(result);
        _userManagerMock.Verify(manager => manager.FindByEmailAsync(email), Times.Once);
        _userManagerMock.Verify(manager => manager.CheckPasswordAsync(appUser, password), Times.Once);
    }

    [Fact]
    public async Task GetUserIdByEmailAsync_ShouldReturnUserId_WhenUserExists()
    {
        // Arrange
        string email = "test@example.com";
        string expectedUserId = "12345";

        var appUser = new ApplicationUser { Email = email, Id = expectedUserId };

        // Configura o mock para encontrar o usuário pelo email
        _userManagerMock
            .Setup(manager => manager.FindByEmailAsync(email))
            .ReturnsAsync(appUser);

        // Act
        var result = await _identityService.GetUserIdByEmailAsync(email);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedUserId, result);
        _userManagerMock.Verify(manager => manager.FindByEmailAsync(email), Times.Once);
    }

    [Fact]
    public async Task GetUserIdByEmailAsync_ShouldReturnNull_WhenUserDoesNotExist()
    {
        // Arrange
        string email = "nonexistent@example.com";

        // Configura o mock para retornar null quando o usuário não for encontrado
        _userManagerMock
            .Setup(manager => manager.FindByEmailAsync(email))
            .ReturnsAsync((ApplicationUser?)null);

        // Act
        var result = await _identityService.GetUserIdByEmailAsync(email);

        // Assert
        Assert.Null(result);
        _userManagerMock.Verify(manager => manager.FindByEmailAsync(email), Times.Once);
    }
}