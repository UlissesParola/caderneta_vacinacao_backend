namespace Core.Entities;

public class RegistroVacina
{
    public string Id { get; set; }
    public DateOnly DataAplicacao { get; set; }
    public DateOnly? DataProximaDose { get; set; }
    public string Lote { get; set; }
    public string Laboratorio { get; set; }
    public string UnidadeSaude { get; set; }
    public string NomeAplicador { get; set; }

    // Relacionamento com Dependente
    public string DependenteId { get; set; }
    public virtual Dependente Dependente { get; set; }

    // Relacionamento com Vacina
    public string VacinaId { get; set; }
    public virtual Vacina Vacina { get; set; }
}