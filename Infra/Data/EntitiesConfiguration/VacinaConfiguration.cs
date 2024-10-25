using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.EntitiesConfiguration;

public class VacinaConfiguration : IEntityTypeConfiguration<Vacina>
{
    public void Configure(EntityTypeBuilder<Vacina> builder)
    {
        builder.ToTable("Vacinas");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.Nome)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(v => v.IdadeMinimaEmMeses)
            .IsRequired();

        builder.Property(v => v.QuantidadeDoses)
            .IsRequired();

        builder.Property(v => v.IntervaloEntreDosesEmDias)
            .IsRequired();

        builder.Property(v => v.Descricao)
            .HasMaxLength(500);
    }
}