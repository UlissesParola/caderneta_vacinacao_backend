using Core.Entities;
using Infra.Data.Repositories.Commands;
using Infra.Identity;
using Infra.InfraServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Tests.InfraService;

public class IdentityServiceIntegrationTests : IntegrationTestBase
{
    private readonly IdentityService _identityService;
    private readonly UsuarioCommandRepository _usuarioCommandRepository;

    public IdentityServiceIntegrationTests()
    {
        // Inicializando o serviço IdentityService usando as dependências configuradas na classe base
        _identityService = new IdentityService(_userManager, _signInManager);

        // Inicializando o repositório de comandos para criação de usuários
        _usuarioCommandRepository = new UsuarioCommandRepository(_userManager, _context);
    }

    [Fact]
    public async Task ValidateUserCredentialsAsync_ShouldReturnTrue_WhenCredentialsAreValid()
    {
        // Arrange
        var email = "test.user@example.com";
        var password = "SecurePassword123";
        var usuario = new Usuario
        {
            Id = "1",
            Nome = "Test",
            Sobrenome = "User",
            Cpf = "12345678901",
            Email = email,
            DataNascimento = new DateOnly(1990, 1, 1),
            Sexo = "Masculino",
            UsuarioDependente = new List<UsuarioDependente>()
        };

        // Criar o usuário usando o repositório
        var createResult = await _usuarioCommandRepository.CreateUsuarioAsync(usuario, password);
        Assert.True(createResult);

        // Act
        var isValid = await _identityService.ValidateUserCredentialsAsync(email, password);

        // Assert
        Assert.True(isValid);
    }

    [Fact]
    public async Task ValidateUserCredentialsAsync_ShouldReturnFalse_WhenCredentialsAreInvalid()
    {
        // Arrange
        var email = "test.user@example.com";
        var password = "SecurePassword123";
        var usuario = new Usuario
        {
            Id = "2",
            Nome = "Test",
            Sobrenome = "User",
            Cpf = "98765432100",
            Email = email,
            DataNascimento = new DateOnly(1990, 1, 1),
            Sexo = "Masculino",
            UsuarioDependente = new List<UsuarioDependente>()
        };

        // Criar o usuário usando o repositório
        var createResult = await _usuarioCommandRepository.CreateUsuarioAsync(usuario, password);
        Assert.True(createResult);

        // Act
        var isValid = await _identityService.ValidateUserCredentialsAsync(email, "WrongPassword");

        // Assert
        Assert.False(isValid);
    }

    [Fact]
    public async Task GetUserIdByEmailAsync_ShouldReturnUserId_WhenUserExists()
    {
        // Arrange
        var email = "test.user2@example.com";
        var password = "SecurePassword123";
        var usuario = new Usuario
        {
            Id = "3",
            Nome = "Test",
            Sobrenome = "User",
            Cpf = "32165498701",
            Email = email,
            DataNascimento = new DateOnly(1990, 1, 1),
            Sexo = "Feminino",
            UsuarioDependente = new List<UsuarioDependente>()
        };

        // Criar o usuário usando o repositório
        var createResult = await _usuarioCommandRepository.CreateUsuarioAsync(usuario, password);
        Assert.True(createResult);

        // Act
        var userId = await _identityService.GetUserIdByEmailAsync(email);

        // Assert
        Assert.NotNull(userId);
        Assert.Equal(usuario.ApplicationUserId, userId);
    }

    [Fact]
    public async Task GetUserByIdAsync_ShouldReturnApplicationUserDto_WhenUserExists()
    {
        // Arrange
        var email = "test.user3@example.com";
        var password = "SecurePassword123";
        var usuario = new Usuario
        {
            Id = "4",
            Nome = "Test",
            Sobrenome = "User",
            Cpf = "85296374100",
            Email = email,
            DataNascimento = new DateOnly(1990, 1, 1),
            Sexo = "Masculino",
            UsuarioDependente = new List<UsuarioDependente>()
        };

        // Criar o usuário usando o repositório
        var createResult = await _usuarioCommandRepository.CreateUsuarioAsync(usuario, password);
        Assert.True(createResult);

        // Act
        var userDto = await _identityService.GetUserByIdAsync(usuario.ApplicationUserId);

        // Assert
        Assert.NotNull(userDto);
        Assert.Equal(usuario.ApplicationUserId, userDto.Id);
        Assert.Equal(email, userDto.Email);
    }

    [Fact]
    public async Task SignInAsync_ShouldReturnTrue_WhenCredentialsAreValid()
    {
        // Arrange
        var email = "test.user4@example.com";
        var password = "SecurePassword123";
        var usuario = new Usuario
        {
            Id = "5",
            Nome = "Test",
            Sobrenome = "User",
            Cpf = "75395145685",
            Email = email,
            DataNascimento = new DateOnly(1990, 1, 1),
            Sexo = "Feminino",
            UsuarioDependente = new List<UsuarioDependente>()
        };

        // Criar o usuário usando o repositório
        var createResult = await _usuarioCommandRepository.CreateUsuarioAsync(usuario, password);
        Assert.True(createResult);

        // Configurar manualmente o HttpContext para o SignInManager
        var httpContext = new DefaultHttpContext
        {
            RequestServices = _serviceProvider // Força o uso do ServiceProvider configurado
        };
        var contextAccessor = _serviceProvider.GetRequiredService<IHttpContextAccessor>();
        contextAccessor.HttpContext = httpContext;

        // Validar que o serviço de autenticação está disponível no contexto
        var authService = httpContext.RequestServices.GetService<IAuthenticationService>();
        Assert.NotNull(authService); // Verifique se o serviço de autenticação está presente

        // Act
        var isSignedIn = await _identityService.SignInAsync(email, password);

        // Assert
        Assert.True(isSignedIn);
    }

    [Fact]
    public async Task SignInAsync_ShouldReturnFalse_WhenCredentialsAreInvalid()
    {
        // Arrange
        var email = "test.user5@example.com";
        var password = "SecurePassword123";
        var usuario = new Usuario
        {
            Id = "6",
            Nome = "Test",
            Sobrenome = "User",
            Cpf = "15975385246",
            Email = email,
            DataNascimento = new DateOnly(1990, 1, 1),
            Sexo = "Masculino",
            UsuarioDependente = new List<UsuarioDependente>()
        };

        // Criar o usuário usando o repositório
        var createResult = await _usuarioCommandRepository.CreateUsuarioAsync(usuario, password);
        Assert.True(createResult);

        // Act
        var isSignedIn = await _identityService.SignInAsync(email, "WrongPassword");

        // Assert
        Assert.False(isSignedIn);
    }
}