using Core.Dto;
using Core.Entities;
using Core.Interfaces.InfraServices;
using Core.Interfaces.Repositories.Queries;
using Dapper;
using System.Data;

namespace Infra.Data.Repositories.Queries;

public class UsuarioQueryRepository : IUsuarioQueryRepository
{
    private readonly IDbConnection _dbConnection;
    private readonly IIdentityService _identityService;

    public UsuarioQueryRepository(
        IDbConnection dbConnection,
        IIdentityService identityService)
    {
        _dbConnection = dbConnection;
        _identityService = identityService;
    }

    public async Task<Usuario?> GetUsuarioByEmailAsync(string email)
    {
        // Consulta o ApplicationUser no Identity usando o IdentityService
        var applicationUserId = await _identityService.GetUserIdByEmailAsync(email);
        if (applicationUserId == null) return null;

        // Consulta o Usuario no banco de dados usando Dapper
        var sql = "SELECT * FROM Usuarios WHERE ApplicationUserId = @ApplicationUserId";
        var usuario = await _dbConnection.QueryFirstOrDefaultAsync<Usuario>(sql, new { ApplicationUserId = applicationUserId });
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
        // Validar as credenciais usando o IdentityService
        return await _identityService.SignInAsync(email, password);
    }

    public async Task<ApplicationUserDto?> GetApplicationUserByIdAsync(string applicationUserId)
    {
        // Consulta o ApplicationUser usando o IdentityService
        return await _identityService.GetUserByIdAsync(applicationUserId);
    }
}