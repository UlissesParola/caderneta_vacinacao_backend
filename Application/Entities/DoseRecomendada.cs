namespace Core.Entities;

public class DoseRecomendada
{
    public Guid Id { get; set; }
    public int Numero { get; set; } // Número da dose (ex: 1, 2, 3...)
    public int IdadeParaAplicacaoEmMeses { get; set; } // Idade recomendada para aplicação
    public Guid VacinaId { get; set; }
    public Vacina Vacina { get; set; }
}