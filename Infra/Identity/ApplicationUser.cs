using Core.Entities;
using Microsoft.AspNetCore.Identity;


namespace Infra.Identity;

public class ApplicationUser : IdentityUser
{
    public virtual Usuario Usuario { get; set; }
}