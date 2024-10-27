using Core.Dto;
using Core.Entities;

namespace Core.Interfaces.Repositories.Queries;

public interface IUsuarioQueryRepository
{
    Task<Usuario?> GetUsuarioByEmailAsync(string email);
    Task<Usuario?> GetUsuarioByIdAsync(string usuarioId);
    Task<bool> ValidateUsuarioCredentialsAsync(string email, string password);

    // Método para obter informações do ApplicationUser no formato DTO
    Task<ApplicationUserDto?> GetApplicationUserByIdAsync(string applicationUserId);
}
