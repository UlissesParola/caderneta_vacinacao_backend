using Api.Requests;
using FluentValidation;

namespace Api.Validators;

public class CreateUsuarioRequestValidator : AbstractValidator<CreateUsuarioRequest>
{
    public CreateUsuarioRequestValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O Nome é obrigatório.")
            .MaximumLength(100).WithMessage("O Nome não pode ter mais de 100 caracteres.");

        RuleFor(x => x.Sobrenome)
            .NotEmpty().WithMessage("O Sobrenome é obrigatório.")
            .MaximumLength(100).WithMessage("O Sobrenome não pode ter mais de 100 caracteres.");

        RuleFor(x => x.Cpf)
            .NotEmpty().WithMessage("O CPF é obrigatório.")
            .Length(11).WithMessage("O CPF deve ter exatamente 11 dígitos.")
            .Matches(@"^\d{11}$").WithMessage("O CPF deve conter apenas números.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O Email é obrigatório.")
            .EmailAddress().WithMessage("O Email fornecido não é válido.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("A senha é obrigatória.")
            .MinimumLength(8).WithMessage("A senha deve ter pelo menos 8 caracteres.");

        RuleFor(x => x.DataNascimento)
            .NotEmpty().WithMessage("A Data de Nascimento é obrigatória.")
            .LessThan(DateOnly.FromDateTime(DateTime.Now)).WithMessage("A Data de Nascimento deve ser no passado.");

        RuleFor(x => x.Sexo)
            .NotEmpty().WithMessage("O Sexo é obrigatório.")
            .Must(value => value == "Masculino" || value == "Feminino" || value == "Outro")
            .WithMessage("O Sexo deve ser 'Masculino', 'Feminino' ou 'Outro'.");
    }
}