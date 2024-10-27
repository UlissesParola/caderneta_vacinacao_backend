using Core.Entities;
using Core.Interfaces.InfraServices;
using Infra.Data.Repositories.Commands;
using Infra.Data.Repositories.Queries;
using Moq;

namespace Infra.Tests.Data.Repositories.Queries;

public class UsuarioQueryRepositoryTests : IntegrationTestBase
{
    private readonly UsuarioQueryRepository _usuarioQueryRepository;
    private readonly UsuarioCommandRepository _usuarioCommandRepository; // Inicialize o UsuarioCommandRepository
    private readonly Mock<IIdentityService> _identityServiceMock;

    public UsuarioQueryRepositoryTests()
    {
        // Criação do mock do IIdentityService
        _identityServiceMock = new Mock<IIdentityService>();

        // Inicializa o repositório com o mock do IIdentityService
        _usuarioQueryRepository = new UsuarioQueryRepository(_dbConnection, _identityServiceMock.Object);

        // Inicializa o UsuarioCommandRepository com as dependências necessárias
        _usuarioCommandRepository = new UsuarioCommandRepository(_userManager, _context);
    }

    [Fact]
    public async Task GetUsuarioByEmailAsync_DeveRetornarUsuario_QuandoEmailExistir()
    {
        // Arrange: cria um usuário de teste no banco de dados
        var email = "test@example.com";
        var usuario = await CriarUsuarioDeTesteAsync(email);

        // Configura o mock para retornar o ID do usuário quando o e-mail é consultado
        _identityServiceMock
            .Setup(service => service.GetUserIdByEmailAsync(email))
            .ReturnsAsync(usuario.ApplicationUserId);

        // Act: tenta recuperar o usuário pelo e-mail
        var resultado = await _usuarioQueryRepository.GetUsuarioByEmailAsync(email);

        // Assert: verifica se o usuário foi recuperado corretamente
        Assert.NotNull(resultado);
        Assert.Equal(usuario.Id, resultado.Id);
        Assert.Equal(email, resultado.Email);
    }

    [Fact]
    public async Task GetUsuarioByEmailAsync_DeveRetornarNull_QuandoEmailNaoExistir()
    {
        // Arrange: configura o mock para retornar null quando o e-mail não existe
        _identityServiceMock
            .Setup(service => service.GetUserIdByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync((string?)null);

        // Act: tenta recuperar um usuário com um e-mail que não existe
        var resultado = await _usuarioQueryRepository.GetUsuarioByEmailAsync("email_inexistente@example.com");

        // Assert: verifica se o resultado é null
        Assert.Null(resultado);
    }

    [Fact]
    public async Task GetUsuarioByIdAsync_DeveRetornarUsuario_QuandoIdExistir()
    {
        // Arrange: cria um usuário de teste no banco de dados
        var usuario = await CriarUsuarioDeTesteAsync("outroteste@example.com");

        // Act: tenta recuperar o usuário pelo ID
        var resultado = await _usuarioQueryRepository.GetUsuarioByIdAsync(usuario.Id);

        // Assert: verifica se o usuário foi recuperado corretamente
        Assert.NotNull(resultado);
        Assert.Equal(usuario.Id, resultado.Id);
    }

    [Fact]
    public async Task GetUsuarioByIdAsync_DeveRetornarNull_QuandoIdNaoExistir()
    {
        // Act: tenta recuperar um usuário com um ID que não existe
        var resultado = await _usuarioQueryRepository.GetUsuarioByIdAsync("id_inexistente");

        // Assert: verifica se o resultado é null
        Assert.Null(resultado);
    }

    [Fact]
    public async Task ValidateUsuarioCredentialsAsync_DeveRetornarTrue_QuandoCredenciaisForemValidas()
    {
        // Arrange: cria um usuário de teste no banco de dados com senha
        var email = "validuser@example.com";
        var senha = "Senha@123";
        await CriarUsuarioDeTesteAsync(email, senha);

        // Configura o mock para validar as credenciais corretamente
        _identityServiceMock
            .Setup(service => service.SignInAsync(email, senha))
            .ReturnsAsync(true);

        // Act: tenta validar as credenciais
        var resultado = await _usuarioQueryRepository.ValidateUsuarioCredentialsAsync(email, senha);

        // Assert: verifica se a validação foi bem-sucedida
        Assert.True(resultado);
    }

    [Fact]
    public async Task ValidateUsuarioCredentialsAsync_DeveRetornarFalse_QuandoCredenciaisForemInvalidas()
    {
        // Arrange: cria um usuário de teste no banco de dados com senha
        var email = "invaliduser@example.com";
        var senhaCorreta = "Senha@123";
        await CriarUsuarioDeTesteAsync(email, senhaCorreta);

        // Configura o mock para validar as credenciais incorretamente
        _identityServiceMock
            .Setup(service => service.SignInAsync(email, "SenhaIncorreta"))
            .ReturnsAsync(false);

        // Act: tenta validar as credenciais com uma senha incorreta
        var resultado = await _usuarioQueryRepository.ValidateUsuarioCredentialsAsync(email, "SenhaIncorreta");

        // Assert: verifica se a validação falhou
        Assert.False(resultado);
    }

    // Método auxiliar para criar um usuário de teste no banco de dados usando o UsuarioCommandRepository
    private async Task<Usuario> CriarUsuarioDeTesteAsync(string email, string senha = "Senha@123")
    {
        // Cria um novo objeto Usuario com todos os campos obrigatórios preenchidos
        var usuario = new Usuario
        {
            Id = Guid.NewGuid().ToString(),
            Email = email,
            Nome = "Teste",
            Sobrenome = "Usuario",
            Cpf = "12345678901",
            DataNascimento = new DateOnly(1990, 1, 1),
            Sexo = "Masculino"
        };

        // Usa o UsuarioCommandRepository para criar o usuário
        var criadoComSucesso = await _usuarioCommandRepository.CreateUsuarioAsync(usuario, senha);
        if (!criadoComSucesso)
            throw new Exception("Falha ao criar o usuário de teste");

        return usuario;
    }
}
