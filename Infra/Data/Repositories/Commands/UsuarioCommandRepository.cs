using Core.Entities;
using Core.Interfaces.Repositories.Commands;
using Core.Utils;
using Infra.Data.Context;
using Infra.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories.Commands;

public class UsuarioCommandRepository : IUsuarioCommandRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly AppDbContext _context;

    public UsuarioCommandRepository(UserManager<ApplicationUser> userManager, AppDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<Result<bool>> CreateUsuarioAsync(Usuario usuario, string password)
    {
        // Criar o ApplicationUser para autenticação
        var applicationUser = new ApplicationUser
        {
            UserName = usuario.Email,
            Email = usuario.Email
        };

        // Criar o ApplicationUser no Identity
        var result = await _userManager.CreateAsync(applicationUser, password);
        if (!result.Succeeded)
            return Result<bool>.Failure(result.ToString(), 400);

        try
        {
            // Associar o ApplicationUser ao Usuario de domínio
            usuario.ApplicationUserId = applicationUser.Id;

            // Criar o Usuario no banco de dados
            _context.Set<Usuario>().Add(usuario);
            var saved = await _context.SaveChangesAsync() > 0;
            return saved ? Result<bool>.Success(saved) : Result<bool>.Failure(saved.ToString(), 400);
        }
        catch (Exception ex)
        {
            // Em caso de falha, remover o ApplicationUser
            await _userManager.DeleteAsync(applicationUser);
            return Result<bool>.Failure(ex.Message, 500);
        }
    }

    public async Task<bool> UpdateUsuarioAsync(Usuario usuario)
    {
        // Atualizar dados no ApplicationUser (Identity)
        var applicationUser = await _userManager.FindByIdAsync(usuario.ApplicationUserId);
        if (applicationUser == null)
            return false;

        applicationUser.Email = usuario.Email;
        applicationUser.UserName = usuario.Email;

        var updateResult = await _userManager.UpdateAsync(applicationUser);
        if (!updateResult.Succeeded)
            return false;

        // Atualizar dados no Usuario (domínio)
        _context.Set<Usuario>().Update(usuario);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteUsuarioAsync(string usuarioId)
    {
        // Buscar o Usuario e ApplicationUser associados
        var usuario = await _context.Set<Usuario>().FirstOrDefaultAsync(u => u.Id == usuarioId);
        if (usuario == null)
            return false;

        var applicationUser = await _userManager.FindByIdAsync(usuario.ApplicationUserId);
        if (applicationUser == null)
            return false;

        try
        {
            // Remover ApplicationUser (Identity)
            var deleteResult = await _userManager.DeleteAsync(applicationUser);
            if (!deleteResult.Succeeded)
                return false;

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> ChangePasswordAsync(string usuarioId, string newPassword)
    {
        // Buscar o ApplicationUser associado
        var usuario = await _context.Set<Usuario>().FirstOrDefaultAsync(u => u.Id == usuarioId);
        if (usuario == null)
            return false;

        var applicationUser = await _userManager.FindByIdAsync(usuario.ApplicationUserId);
        if (applicationUser == null)
            return false;

        // Alterar senha no Identity
        var token = await _userManager.GeneratePasswordResetTokenAsync(applicationUser);
        var result = await _userManager.ResetPasswordAsync(applicationUser, token, newPassword);
        return result.Succeeded;
    }
}
