using Core.Entities;
using Infra.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Context;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Dependente> Dependentes { get; set; }
    public DbSet<Vacina> Vacinas { get; set; }
    public DbSet<RegistroVacina> RegistrosVacinas { get; set; }
    public DbSet<UsuarioDependente> UsuariosDependentes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Aplicando as configurações de cada entidade
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}