using Core.Dto;
using Core.Interfaces.InfraServices;
using Infra.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infra.InfraServices;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public IdentityService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
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

    public async Task<ApplicationUserDto?> GetUserByIdAsync(string userId)
    {
        var appUser = await _userManager.FindByIdAsync(userId);
        if (appUser == null)
            return null;

        return new ApplicationUserDto
        {
            Id = appUser.Id,
            Email = appUser.Email,
            UserName = appUser.UserName
        };
    }

    public async Task<bool> SignInAsync(string email, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(email, password, false, false);
        return result.Succeeded;
    }
}