using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.EntitiesConfiguration;

public class DependenteConfiguration : IEntityTypeConfiguration<Dependente>
{
    public void Configure(EntityTypeBuilder<Dependente> builder)
    {
        builder.ToTable("Dependentes");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Nome)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(d => d.Sobrenome)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(d => d.Cpf)
            .HasMaxLength(11)
            .IsRequired();

        builder.Property(d => d.DataNascimento)
            .IsRequired();

        builder.Property(d => d.Sexo)
            .HasMaxLength(20);

        // Relacionamento com UserDependentes
        builder.HasMany(d => d.UsuarioDependente)
            .WithOne(ud => ud.Dependente)
            .HasForeignKey(ud => ud.DependenteId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relacionamento com RegistroVacina
        builder.HasMany(d => d.RegistrosVacinas)
            .WithOne(rv => rv.Dependente)
            .HasForeignKey(rv => rv.DependenteId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}