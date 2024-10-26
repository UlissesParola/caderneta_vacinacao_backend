using Core.Entities;

namespace Core.Interfaces.Repositories.Queries;

public interface IUsuarioQueryRepository
{
    Task<Usuario> GetUsuarioByEmailAsync(string email);
    Task<Usuario> GetUsuarioByIdAsync(string usuarioId);
    Task<bool> ValidateUsuarioCredentialsAsync(string email, string password);
}
