namespace Core.Entities;

public class Vacina
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string? Descricao { get; set; }
    public List<DoseRecomendada> DosesRecomendadas { get; set; } = new List<DoseRecomendada>();
}