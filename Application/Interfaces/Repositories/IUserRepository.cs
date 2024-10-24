using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User> GetUserByIdAsync(string userId);
    Task<User> GetUserByEmailAsync(string email);
    Task<bool> CreateUserAsync(User user, string password);
    Task<bool> ValidateUserCredentialsAsync(string email, string password);

}
