using Core.Entities;
using Core.Interfaces.Repositories.Queries;
using Infra.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infra.Data.Repositories.Queries;

public class UsuarioQueryRepository : IUsuarioQueryRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public UsuarioQueryRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<Usuario> GetUsuarioByEmailAsync(string email)
    {
        var applicationUser = await _userManager.FindByEmailAsync(email);
        if (applicationUser == null) return null;

        return new Usuario
        {
            Id = applicationUser.Id,
            Email = applicationUser.Email
        };
    }

    public async Task<Usuario> GetUsuarioByIdAsync(string usuarioId)
    {
        var applicationUser = await _userManager.FindByIdAsync(usuarioId);
        if (applicationUser == null) return null;

        return new Usuario
        {
            Id = applicationUser.Id,
            Email = applicationUser.Email
        };
    }

    public async Task<bool> ValidateUsuarioCredentialsAsync(string email, string password)
    {
        var applicationUser = await _userManager.FindByEmailAsync(email);
        if (applicationUser == null) return false;

        var result = await _signInManager.PasswordSignInAsync(email, password, false, false);
        return result.Succeeded;
    }
}