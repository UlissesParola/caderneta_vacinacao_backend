using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.EntitiesConfiguration;

public class DoseRecomendadaConfiguration : IEntityTypeConfiguration<DoseRecomendada>
{
    public void Configure(EntityTypeBuilder<DoseRecomendada> builder)
    {
        builder.ToTable("dosesrecomendadas");

        builder.HasKey(dr => dr.Id);

        builder.Property(dr => dr.Id)
            .IsRequired();

        builder.Property(dr => dr.Numero)
            .IsRequired();

        builder.Property(dr => dr.IdadeParaAplicacaoEmMeses)
            .IsRequired();

        // Seed Data para Doses Recomendadas
        builder.HasData(
            // Doses para BCG
            new DoseRecomendada { Id = Guid.NewGuid(), Numero = 1, IdadeParaAplicacaoEmMeses = 0, VacinaId = VacinaConfiguration.BcgId },

            // Doses para Hepatite B
            new DoseRecomendada { Id = Guid.NewGuid(), Numero = 1, IdadeParaAplicacaoEmMeses = 0, VacinaId = VacinaConfiguration.HepatiteBId },
            new DoseRecomendada { Id = Guid.NewGuid(), Numero = 2, IdadeParaAplicacaoEmMeses = 1, VacinaId = VacinaConfiguration.HepatiteBId },
            new DoseRecomendada { Id = Guid.NewGuid(), Numero = 3, IdadeParaAplicacaoEmMeses = 6, VacinaId = VacinaConfiguration.HepatiteBId },

            // Doses para Pentavalente
            new DoseRecomendada { Id = Guid.NewGuid(), Numero = 1, IdadeParaAplicacaoEmMeses = 2, VacinaId = VacinaConfiguration.PentavalenteId },
            new DoseRecomendada { Id = Guid.NewGuid(), Numero = 2, IdadeParaAplicacaoEmMeses = 4, VacinaId = VacinaConfiguration.PentavalenteId },
            new DoseRecomendada { Id = Guid.NewGuid(), Numero = 3, IdadeParaAplicacaoEmMeses = 6, VacinaId = VacinaConfiguration.PentavalenteId },

            // Doses para Rotavírus
            new DoseRecomendada { Id = Guid.NewGuid(), Numero = 1, IdadeParaAplicacaoEmMeses = 2, VacinaId = VacinaConfiguration.RotavirusId },
            new DoseRecomendada { Id = Guid.NewGuid(), Numero = 2, IdadeParaAplicacaoEmMeses = 4, VacinaId = VacinaConfiguration.RotavirusId },

            // Doses para Pneumocócica 10-valente
            new DoseRecomendada { Id = Guid.NewGuid(), Numero = 1, IdadeParaAplicacaoEmMeses = 2, VacinaId = VacinaConfiguration.Pneumo10Id },
            new DoseRecomendada { Id = Guid.NewGuid(), Numero = 2, IdadeParaAplicacaoEmMeses = 4, VacinaId = VacinaConfiguration.Pneumo10Id },
            new DoseRecomendada { Id = Guid.NewGuid(), Numero = 3, IdadeParaAplicacaoEmMeses = 12, VacinaId = VacinaConfiguration.Pneumo10Id },

            // Doses para Meningocócica C
            new DoseRecomendada { Id = Guid.NewGuid(), Numero = 1, IdadeParaAplicacaoEmMeses = 3, VacinaId = VacinaConfiguration.MeningococicaCId },
            new DoseRecomendada { Id = Guid.NewGuid(), Numero = 2, IdadeParaAplicacaoEmMeses = 5, VacinaId = VacinaConfiguration.MeningococicaCId },

            // Dose única para Febre Amarela
            new DoseRecomendada { Id = Guid.NewGuid(), Numero = 1, IdadeParaAplicacaoEmMeses = 9, VacinaId = VacinaConfiguration.FebreAmarelaId },

            // Doses para Tríplice Viral
            new DoseRecomendada { Id = Guid.NewGuid(), Numero = 1, IdadeParaAplicacaoEmMeses = 12, VacinaId = VacinaConfiguration.TripliceViralId },
            new DoseRecomendada { Id = Guid.NewGuid(), Numero = 2, IdadeParaAplicacaoEmMeses = 15, VacinaId = VacinaConfiguration.TripliceViralId },

            // Dose única para Hepatite A
            new DoseRecomendada { Id = Guid.NewGuid(), Numero = 1, IdadeParaAplicacaoEmMeses = 12, VacinaId = VacinaConfiguration.HepatiteAId },

            // Dose única para Tetraviral
            new DoseRecomendada { Id = Guid.NewGuid(), Numero = 1, IdadeParaAplicacaoEmMeses = 15, VacinaId = VacinaConfiguration.TetraviralId },

            // Doses para DTP
            new DoseRecomendada { Id = Guid.NewGuid(), Numero = 1, IdadeParaAplicacaoEmMeses = 15, VacinaId = VacinaConfiguration.DtpId },
            new DoseRecomendada { Id = Guid.NewGuid(), Numero = 2, IdadeParaAplicacaoEmMeses = 48, VacinaId = VacinaConfiguration.DtpId },
            new DoseRecomendada { Id = Guid.NewGuid(), Numero = 3, IdadeParaAplicacaoEmMeses = 144, VacinaId = VacinaConfiguration.DtpId }, // Reforço aos 4 e 12 anos

            // Dose única para Varicela
            new DoseRecomendada { Id = Guid.NewGuid(), Numero = 1, IdadeParaAplicacaoEmMeses = 15, VacinaId = VacinaConfiguration.VaricelaId }
        );

        // Relacionamento com Vacina
        builder.HasOne(dr => dr.Vacina)
            .WithMany(v => v.DosesRecomendadas)
            .HasForeignKey(dr => dr.VacinaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}