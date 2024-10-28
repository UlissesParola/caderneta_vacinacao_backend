using Core.Entities;
using Core.Utils;

namespace Core.Interfaces.Repositories.Commands;

public interface IUsuarioCommandRepository
{
    Task<Result<bool>> CreateUsuarioAsync(Usuario usuario, string password);
    Task<bool> UpdateUsuarioAsync(Usuario usuario);
    Task<bool> DeleteUsuarioAsync(string usuarioId);
    Task<bool> ChangePasswordAsync(string usuarioId, string newPassword);
}
