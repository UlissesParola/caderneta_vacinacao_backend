using Core.Interfaces.InfraServices;
using Infra.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infra.InfraServices;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentityService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> ValidateUserCredentialsAsync(string email, string password)
    {
        var appUser = await _userManager.FindByEmailAsync(email);
        if (appUser == null)
        {
            return false;
        }
        return await _userManager.CheckPasswordAsync(appUser, password);
    }

    public async Task<string?> GetUserIdByEmailAsync(string email)
    {
        var appUser = await _userManager.FindByEmailAsync(email);
        return appUser?.Id.ToString();
    }
}