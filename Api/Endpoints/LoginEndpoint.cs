using Core.Interfaces.InfraServices;
using Core.Interfaces.Services;
using Core.Utils;
using FastEndpoints;
using Microsoft.AspNetCore.Identity.Data;

namespace Api.Endpoints;

public class LoginEndpoint : Endpoint<LoginRequest, string>
{
    private readonly IAuthService _authService;
    private readonly ITokenService _tokenService;

    public LoginEndpoint(IAuthService authService, ITokenService tokenService)
    {
        _authService = authService;
        _tokenService = tokenService;
    }

    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("/api/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {
        // Valida as credenciais do usuário
        var isValidUser = await _authService.ValidateUserCredentialsAsync(req.Email, req.Password);
        if (!isValidUser)
        {
            await SendAsync("Credenciais inválidas.", statusCode: 401, ct);
            return;
        }

        // Recupera o usuário pelo e-mail
        var user = await _authService.GetUserByEmailAsync(req.Email);
        if (user == null)
        {
            await SendAsync("Usuário não encontrado.", statusCode: 404, ct);
            return;
        }

        // Gera o token JWT
        var tokenResult = _tokenService.GenerateToken(user);
        if (!tokenResult.IsSuccess)
        {
            await SendAsync(tokenResult.ErrorMessage, statusCode: tokenResult.StatusCode, ct);
            return;
        }

        await SendAsync(tokenResult.Value, cancellation: ct);
    }
}

public record LoginResponse(string Token);
