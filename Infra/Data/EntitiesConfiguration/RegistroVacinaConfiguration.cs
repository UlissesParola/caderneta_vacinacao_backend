using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.EntitiesConfiguration;

public class RegistroVacinaConfiguration : IEntityTypeConfiguration<RegistroVacina>
{
    public void Configure(EntityTypeBuilder<RegistroVacina> builder)
    {
        builder.ToTable("RegistrosVacinas");

        builder.HasKey(rv => rv.Id);

        builder.Property(rv => rv.DataAplicacao)
            .IsRequired();

        builder.Property(rv => rv.DataProximaDose);

        builder.Property(rv => rv.Lote)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(rv => rv.Laboratorio)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(rv => rv.UnidadeSaude)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(rv => rv.NomeAplicador)
            .HasMaxLength(100)
            .IsRequired();

        // Relacionamento com Dependente
        builder.HasOne(rv => rv.Dependente)
            .WithMany(d => d.RegistrosVacinas)
            .HasForeignKey(rv => rv.DependenteId);

        // Relacionamento com Vacina
        builder.HasOne(rv => rv.Vacina)
            .WithMany()
            .HasForeignKey(rv => rv.VacinaId);

        // Relacionamento com DoseRecomendada
        builder.HasOne(rv => rv.DoseRecomendada)
            .WithMany()
            .HasForeignKey(rv => rv.DoseRecomendadaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}