namespace Core.Entities;

public class RegistroVacina
{
    public Guid Id { get; set; }
    public DateOnly DataAplicacao { get; set; }
    public DateOnly? DataProximaDose { get; set; }
    public string Lote { get; set; }
    public string Laboratorio { get; set; }
    public string UnidadeSaude { get; set; }
    public string NomeAplicador { get; set; }

    // Relacionamento com Vacina
    public Guid VacinaId { get; set; }
    public virtual Vacina Vacina { get; set; }

    // Relacionamento com a dose recomendada
    public Guid DoseRecomendadaId { get; set; } 
    public DoseRecomendada DoseRecomendada { get; set; }

    // Relacionamento com Dependente
    public Guid DependenteId { get; set; }
    public virtual Dependente Dependente { get; set; }
}