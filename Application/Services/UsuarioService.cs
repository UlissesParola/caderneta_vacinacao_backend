using Core.Dto;
using Core.Entities;
using Core.Interfaces.Repositories.Commands;
using Core.Interfaces.Services;
using Core.Utils;

namespace Core.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioCommandRepository _usuarioCommandRepository;

    public UsuarioService(IUsuarioCommandRepository usuarioCommandRepository)
    {
        _usuarioCommandRepository = usuarioCommandRepository;
    }

    public async Task<Result<bool>> CreateUsuarioAsync(UsuarioDTO usuarioDto)
    {
        var usuario = new Usuario
        {
            Nome = usuarioDto.Nome,
            Sobrenome = usuarioDto.Sobrenome,
            Cpf = usuarioDto.Cpf,
            Email = usuarioDto.Email,
            DataNascimento = usuarioDto.DataNascimento,
            Sexo = usuarioDto.Sexo
        };

        var success = await _usuarioCommandRepository.CreateUsuarioAsync(usuario, usuarioDto.Password);

        if (success)
        {
            return Result<bool>.Success(true);
        }

        return Result<bool>.Failure("Erro ao criar o usuário.", 400);
    }
}