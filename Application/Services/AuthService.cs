using Core.Entities;
using Core.Interfaces.InfraServices;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

namespace Core.Services;

public class AuthService : IAuthService
{
    private readonly IIdentityService _identityService;
    private readonly IUserRepository _userRepository;

    public AuthService(IIdentityService identityService, IUserRepository userRepository)
    {
        _identityService = identityService;
        _userRepository = userRepository;
    }

    public async Task<bool> ValidateUserCredentialsAsync(string email, string password)
    {
        return await _identityService.ValidateUserCredentialsAsync(email, password);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _userRepository.GetUserByEmailAsync(email);
    }
}