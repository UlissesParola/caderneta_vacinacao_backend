namespace Core.Entities;

public class User
{
    public string Id { get; set; }
    public string? Nome { get; set; }
    public string? Sobrenome { get; set; }
    public string? Cpf { get; set; }
    public string? Email { get; set; }
    public DateOnly DataNascimento { get; set; }
    public string? Sexo { get; set; }
    public string ApplicationUserId { get; set; }
}
