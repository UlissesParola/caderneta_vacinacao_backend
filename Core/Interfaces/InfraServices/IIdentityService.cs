
using Core.Dto;

namespace Core.Interfaces.InfraServices;

public interface IIdentityService
{
    Task<bool> ValidateUserCredentialsAsync(string email, string password);
    Task<string?> GetUserIdByEmailAsync(string email);
    Task<ApplicationUserDto?> GetUserByIdAsync(string userId);
    Task<bool> SignInAsync(string email, string password);
}
