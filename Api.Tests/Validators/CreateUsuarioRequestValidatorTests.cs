using Api.Requests;
using Api.Validators;
using FluentValidation.TestHelper;

namespace Api.Tests.Validators;

public class CreateUsuarioRequestValidatorTests
{
    private readonly CreateUsuarioRequestValidator _validator;

    public CreateUsuarioRequestValidatorTests()
    {
        _validator = new CreateUsuarioRequestValidator();
    }

    [Fact]
    public void Should_HaveError_When_NomeIsEmpty()
    {
        // Arrange
        var request = new CreateUsuarioRequest("", "Sobrenome", "12345678901", "test@example.com", "password123", new DateOnly(2000, 1, 1), "Masculino");

        // Act & Assert
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Nome)
            .WithErrorMessage("O Nome é obrigatório.");
    }

    [Fact]
    public void Should_HaveError_When_NomeExceedsMaxLength()
    {
        // Arrange
        var nome = new string('a', 101); // Nome com 101 caracteres
        var request = new CreateUsuarioRequest(nome, "Sobrenome", "12345678901", "test@example.com", "password123", new DateOnly(2000, 1, 1), "Masculino");

        // Act & Assert
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Nome)
            .WithErrorMessage("O Nome não pode ter mais de 100 caracteres.");
    }

    [Fact]
    public void Should_HaveError_When_CpfIsInvalidLength()
    {
        // Arrange
        var request = new CreateUsuarioRequest("Nome", "Sobrenome", "12345", "test@example.com", "password123", new DateOnly(2000, 1, 1), "Masculino");

        // Act & Assert
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Cpf)
            .WithErrorMessage("O CPF deve ter exatamente 11 dígitos.");
    }

    [Fact]
    public void Should_HaveError_When_CpfHasNonNumericCharacters()
    {
        // Arrange
        var request = new CreateUsuarioRequest("Nome", "Sobrenome", "12345678abc", "test@example.com", "password123", new DateOnly(2000, 1, 1), "Masculino");

        // Act & Assert
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Cpf)
            .WithErrorMessage("O CPF deve conter apenas números.");
    }

    [Fact]
    public void Should_HaveError_When_EmailIsInvalid()
    {
        // Arrange
        var request = new CreateUsuarioRequest("Nome", "Sobrenome", "12345678901", "invalid-email", "password123", new DateOnly(2000, 1, 1), "Masculino");

        // Act & Assert
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Email)
            .WithErrorMessage("O Email fornecido não é válido.");
    }

    [Fact]
    public void Should_HaveError_When_PasswordIsTooShort()
    {
        // Arrange
        var request = new CreateUsuarioRequest("Nome", "Sobrenome", "12345678901", "test@example.com", "short", new DateOnly(2000, 1, 1), "Masculino");

        // Act & Assert
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Password)
            .WithErrorMessage("A senha deve ter pelo menos 8 caracteres.");
    }

    [Fact]
    public void Should_HaveError_When_DataNascimentoIsInTheFuture()
    {
        // Arrange
        var futureDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1)); // Data no futuro
        var request = new CreateUsuarioRequest("Nome", "Sobrenome", "12345678901", "test@example.com", "password123", futureDate, "Masculino");

        // Act & Assert
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.DataNascimento)
            .WithErrorMessage("A Data de Nascimento deve ser no passado.");
    }

    [Fact]
    public void Should_HaveError_When_SexoIsInvalid()
    {
        // Arrange
        var request = new CreateUsuarioRequest("Nome", "Sobrenome", "12345678901", "test@example.com", "password123", new DateOnly(2000, 1, 1), "Invalido");

        // Act & Assert
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Sexo)
            .WithErrorMessage("O Sexo deve ser 'Masculino', 'Feminino' ou 'Outro'.");
    }

    [Fact]
    public void Should_PassValidation_When_AllFieldsAreValid()
    {
        // Arrange
        var request = new CreateUsuarioRequest("Nome", "Sobrenome", "12345678901", "test@example.com", "password123", new DateOnly(2000, 1, 1), "Masculino");

        // Act & Assert
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveAnyValidationErrors();
    }
}