using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.EntitiesConfiguration
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Nome)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.Sobrenome)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.Cpf)
                .HasMaxLength(11)
                .IsRequired();

            builder.Property(u => u.Email)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(u => u.DataNascimento)
                .IsRequired();

            builder.Property(u => u.Sexo)
                .HasMaxLength(20);

            // Relacionamento com UserDependentes
            builder.HasMany(u => u.UsuarioDependente)
                .WithOne(ud => ud.Usuario)
                .HasForeignKey(ud => ud.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}