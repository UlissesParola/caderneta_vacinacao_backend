using Api.Requests;
using Core.Dto;
using Core.Interfaces.Services;
using FastEndpoints;

namespace Api.Endpoints;

public class CreateUsuarioEndpoint : Endpoint<CreateUsuarioRequest>
{
    private readonly IUsuarioService _usuarioService;

    public CreateUsuarioEndpoint(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("/api/usuarios");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateUsuarioRequest req, CancellationToken ct)
    {
        var usuarioDto = new UsuarioDTO
        {
            Nome = req.Nome,
            Sobrenome = req.Sobrenome,
            Cpf = req.Cpf,
            Email = req.Email,
            Password = req.Password,
            DataNascimento = req.DataNascimento,
            Sexo = req.Sexo
        };

        var result = await _usuarioService.CreateUsuarioAsync(usuarioDto);

        if (result.IsSuccess)
        {
            await SendOkAsync(new { Message = "Usuário criado com sucesso!" }, ct);
        }
        else
        {
            await SendAsync(new { Message = result.ErrorMessage }, result.StatusCode, ct);
        }
    }
}
