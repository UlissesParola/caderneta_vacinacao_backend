namespace Presentation.Requests;

public record CreateUsuarioRequest(
    string Nome,
    string Sobrenome,
    string Cpf,
    string Email,
    string Password,
    DateOnly DataNascimento,
    string Sexo
);
