using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.EntitiesConfiguration;

public class VacinaConfiguration : IEntityTypeConfiguration<Vacina>
{
    public static readonly Guid BcgId = Guid.NewGuid();
    public static readonly Guid HepatiteBId = Guid.NewGuid();
    public static readonly Guid PentavalenteId = Guid.NewGuid();
    public static readonly Guid RotavirusId = Guid.NewGuid();
    public static readonly Guid Pneumo10Id = Guid.NewGuid();
    public static readonly Guid MeningococicaCId = Guid.NewGuid();
    public static readonly Guid FebreAmarelaId = Guid.NewGuid();
    public static readonly Guid TripliceViralId = Guid.NewGuid();
    public static readonly Guid HepatiteAId = Guid.NewGuid();
    public static readonly Guid TetraviralId = Guid.NewGuid();
    public static readonly Guid DtpId = Guid.NewGuid();
    public static readonly Guid VaricelaId = Guid.NewGuid();

    public void Configure(EntityTypeBuilder<Vacina> builder)
    {
        builder.ToTable("Vacinas");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.Id)
            .IsRequired();

        builder.Property(v => v.Nome)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(v => v.Descricao)
            .HasMaxLength(500);

        // Seed Data para Vacinas
        builder.HasData(
            new Vacina { Id = BcgId, Nome = "BCG", Descricao = "Protege contra formas graves de tuberculose." },
            new Vacina { Id = HepatiteBId, Nome = "Hepatite B", Descricao = "Protege contra hepatite B." },
            new Vacina { Id = PentavalenteId, Nome = "Pentavalente", Descricao = "Protege contra difteria, tétano, coqueluche, hepatite B e Haemophilus influenzae tipo B." },
            new Vacina { Id = RotavirusId, Nome = "Rotavírus", Descricao = "Protege contra gastroenterite causada por rotavírus." },
            new Vacina { Id = Pneumo10Id, Nome = "Pneumocócica 10-valente", Descricao = "Protege contra doenças causadas por pneumococo, como pneumonia, otite e meningite." },
            new Vacina { Id = MeningococicaCId, Nome = "Meningocócica C", Descricao = "Protege contra meningite causada pelo meningococo C." },
            new Vacina { Id = FebreAmarelaId, Nome = "Febre Amarela", Descricao = "Protege contra a febre amarela." },
            new Vacina { Id = TripliceViralId, Nome = "Tríplice Viral (SCR)", Descricao = "Protege contra sarampo, caxumba e rubéola." },
            new Vacina { Id = HepatiteAId, Nome = "Hepatite A", Descricao = "Protege contra hepatite A." },
            new Vacina { Id = TetraviralId, Nome = "Tetraviral", Descricao = "Protege contra sarampo, caxumba, rubéola e varicela." },
            new Vacina { Id = DtpId, Nome = "DTP", Descricao = "Protege contra difteria, tétano e coqueluche." },
            new Vacina { Id = VaricelaId, Nome = "Varicela", Descricao = "Protege contra a varicela (catapora)." }
        );

        // Relacionamento com DoseRecomendada
        builder.HasMany(v => v.DosesRecomendadas)
            .WithOne(dr => dr.Vacina)
            .HasForeignKey(dr => dr.VacinaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}