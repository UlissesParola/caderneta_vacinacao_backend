﻿namespace Core.Entities;

public class UsuarioDependente
{
    public string UsuarioId { get; set; }
    public virtual Usuario Usuario { get; set; }

    public string DependenteId { get; set; }
    public virtual Dependente Dependente { get; set; }
}