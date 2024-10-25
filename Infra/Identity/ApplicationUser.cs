using Core.Entities;
using Microsoft.AspNetCore.Identity;


namespace Infra.Identity;

public class ApplicationUser : IdentityUser
{
    public string UsuarioId { get; set; }
    public virtual Usuario Usuario { get; set; }
}