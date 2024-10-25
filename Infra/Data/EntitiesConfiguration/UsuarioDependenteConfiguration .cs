using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Core.Entities;

namespace Infra.Data.EntitiesConfiguration
{
    public class UsuarioDependenteConfiguration : IEntityTypeConfiguration<UsuarioDependente>
    {
        public void Configure(EntityTypeBuilder<UsuarioDependente> builder)
        {
            builder.ToTable("UsuariosDependentes");

            builder.HasKey(ud => new { ud.UsuarioId, ud.DependenteId });

            builder.HasOne(ud => ud.Usuario)
                .WithMany(u => u.UsuarioDependente)
                .HasForeignKey(ud => ud.UsuarioId);

            builder.HasOne(ud => ud.Dependente)
                .WithMany(d => d.UsuarioDependente)
                .HasForeignKey(ud => ud.DependenteId);
        }
    }
}