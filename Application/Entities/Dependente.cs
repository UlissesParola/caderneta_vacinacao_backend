namespace Core.Entities;

public class Dependente
{
    public string Id { get; set; }
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public string Cpf { get; set; }
    public DateOnly DataNascimento { get; set; }
    public string Sexo { get; set; }

    // Relacionamento com Usuários
    public virtual ICollection<UsuarioDependente> UsuarioDependente { get; set; }

    // Relacionamento com registros de vacinas
    public virtual ICollection<RegistroVacina> RegistrosVacinas { get; set; }
}