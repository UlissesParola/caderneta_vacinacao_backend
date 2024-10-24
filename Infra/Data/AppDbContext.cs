using Core.Entities;
using Infra.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configurações adicionais para a relação entre ApplicationUser e User
        builder.Entity<ApplicationUser>()
                .HasOne(au => au.User)
                .WithOne()
                .HasForeignKey<User>(u => u.ApplicationUserId)  // Configuração da FK no User
                .OnDelete(DeleteBehavior.Cascade);
    }
}