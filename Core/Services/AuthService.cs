using Core.Entities;
using Core.Interfaces.InfraServices;
using Core.Interfaces.Repositories.Queries;
using Core.Interfaces.Services;

namespace Core.Services;

public class AuthService : IAuthService
{
    private readonly IIdentityService _identityService;
    private readonly IUsuarioQueryRepository _usuarioRepository;

    public AuthService(IIdentityService identityService, IUsuarioQueryRepository usuarioRepository)
    {
        _identityService = identityService;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<bool> ValidateUserCredentialsAsync(string email, string password)
    {
        return await _identityService.ValidateUserCredentialsAsync(email, password);
    }

    public async Task<Usuario?> GetUserByEmailAsync(string email)
    {
        return await _usuarioRepository.GetUsuarioByEmailAsync(email);
    }
}