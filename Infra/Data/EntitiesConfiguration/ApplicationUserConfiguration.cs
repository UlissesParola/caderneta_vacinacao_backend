using Core.Entities;
using Infra.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.EntitiesConfiguration;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasOne(a => a.Usuario)
            .WithOne()
            .HasForeignKey<Usuario>(u => u.ApplicationUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}