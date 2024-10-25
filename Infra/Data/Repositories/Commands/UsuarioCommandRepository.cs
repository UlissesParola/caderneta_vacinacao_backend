using Core.Entities;
using Core.Interfaces.Repositories.Commands;
using Infra.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infra.Data.Repositories.Commands;

public class UsuarioCommandRepository : IUsuarioCommandRepository
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UsuarioCommandRepository(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> CreateUsuarioAsync(Usuario usuario, string password)
    {
        var applicationUser = new ApplicationUser
        {
            UserName = usuario.Email,
            Email = usuario.Email,
            UsuarioId = usuario.Id
        };

        var result = await _userManager.CreateAsync(applicationUser, password);
        return result.Succeeded;
    }

    public async Task<bool> UpdateUsuarioAsync(Usuario usuario)
    {
        var applicationUser = await _userManager.FindByIdAsync(usuario.Id);
        if (applicationUser == null)
            return false;

        applicationUser.Email = usuario.Email;
        applicationUser.UserName = usuario.Email;

        var result = await _userManager.UpdateAsync(applicationUser);
        return result.Succeeded;
    }

    public async Task<bool> DeleteUsuarioAsync(string usuarioId)
    {
        var applicationUser = await _userManager.FindByIdAsync(usuarioId);
        if (applicationUser == null)
            return false;

        var result = await _userManager.DeleteAsync(applicationUser);
        return result.Succeeded;
    }

    public async Task<bool> ChangePasswordAsync(string usuarioId, string newPassword)
    {
        var applicationUser = await _userManager.FindByIdAsync(usuarioId);
        if (applicationUser == null)
            return false;

        var token = await _userManager.GeneratePasswordResetTokenAsync(applicationUser);
        var result = await _userManager.ResetPasswordAsync(applicationUser, token, newPassword);
        return result.Succeeded;
    }
}
