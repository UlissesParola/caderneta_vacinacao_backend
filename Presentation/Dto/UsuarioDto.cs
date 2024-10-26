namespace Presentation.Dto;

public class UsuarioDTO
{
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public string Cpf { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateOnly DataNascimento { get; set; }
    public string Sexo { get; set; }
}
