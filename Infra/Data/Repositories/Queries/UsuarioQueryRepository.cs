using Core.Dto;
using Core.Entities;
using Core.Interfaces.Repositories.Queries;
using Dapper;
using Infra.Identity;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace Infra.Data.Repositories.Queries;

public class UsuarioQueryRepository : IUsuarioQueryRepository
{
    private readonly IDbConnection _dbConnection;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public UsuarioQueryRepository(
        IDbConnection dbConnection,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _dbConnection = dbConnection;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<Usuario?> GetUsuarioByEmailAsync(string email)
    {
        // Consulta o ApplicationUser no Identity
        var applicationUser = await _userManager.FindByEmailAsync(email);
        if (applicationUser == null) return null;

        // Consulta o Usuario no banco de dados usando Dapper
        var sql = "SELECT * FROM Usuarios WHERE ApplicationUserId = @ApplicationUserId";
        var usuario = await _dbConnection.QueryFirstOrDefaultAsync<Usuario>(sql, new { ApplicationUserId = applicationUser.Id });
        return usuario;
    }

    public async Task<Usuario?> GetUsuarioByIdAsync(string usuarioId)
    {
        // Consulta o Usuario pelo ID usando Dapper
        var sql = "SELECT * FROM Usuarios WHERE Id = @UsuarioId";
        var usuario = await _dbConnection.QueryFirstOrDefaultAsync<Usuario>(sql, new { UsuarioId = usuarioId });
        return usuario;
    }

    public async Task<bool> ValidateUsuarioCredentialsAsync(string email, string password)
    {
        // Consulta o ApplicationUser no Identity
        var applicationUser = await _userManager.FindByEmailAsync(email);
        if (applicationUser == null) return false;

        // Validar as credenciais usando o SignInManager
        var result = await _signInManager.PasswordSignInAsync(email, password, false, false);
        return result.Succeeded;
    }

    public async Task<ApplicationUserDto?> GetApplicationUserByIdAsync(string applicationUserId)
    {
        // Consulta o ApplicationUser no Identity
        var applicationUser = await _userManager.FindByIdAsync(applicationUserId);
        if (applicationUser == null) return null;

        // Retorna um DTO para evitar dependência da camada Infra no Core
        return new ApplicationUserDto
        {
            Id = applicationUser.Id,
            Email = applicationUser.Email,
            UserName = applicationUser.UserName
        };
    }
}