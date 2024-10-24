using Core.Entities;
using Microsoft.AspNetCore.Identity;


namespace Infra.Identity;

public class ApplicationUser : IdentityUser
{
    public string UserId { get; set; }
    public virtual User User { get; set; }
}