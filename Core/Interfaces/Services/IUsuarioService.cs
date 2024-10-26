using Core.Dto;
using Core.Utils;

namespace Core.Interfaces.Services;

public interface IUsuarioService
{
    Task<Result<bool>> CreateUsuarioAsync(UsuarioDTO usuarioDto);
}