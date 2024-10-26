
namespace Core.Interfaces.InfraServices;

public interface IIdentityService
{
    Task<bool> ValidateUserCredentialsAsync(string email, string password);
    Task<string?> GetUserIdByEmailAsync(string email);
}
