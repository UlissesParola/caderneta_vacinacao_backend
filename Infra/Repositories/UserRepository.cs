using Core.Entities;
using Core.Interfaces.Repositories;
using Infra.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infra.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public UserRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public Task<bool> CreateUserAsync(User user, string password)
    {
        throw new NotImplementedException();
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        var user = await _userManager.FindByNameAsync(email);
        if (user == null) return null;

        return new User { Email = user.Email };
    }

    public async Task<User> GetUserByIdAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null) return null;

        return new User { Email = user.Email};
    }

    public async Task<bool> ValidateUserCredentialsAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) return false;

        var result = await _signInManager.PasswordSignInAsync(email, password, false, false);
        return result.Succeeded;
    }
}