namespace Core.Entities;

public class Vacina
{
    public string Id { get; set; }
    public string Nome { get; set; }
    public int IdadeMinimaEmMeses { get; set; } // Idade mínima para aplicação em meses
    public int QuantidadeDoses { get; set; }
    public int IntervaloEntreDosesEmDias { get; set; } // Em dias
    public string? Descricao { get; set; }
}