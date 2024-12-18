﻿
using Core.Entities;

namespace Core.Interfaces.Services;

public interface IAuthService
{
    Task<bool> ValidateUserCredentialsAsync(string email, string password);
    Task<Usuario?> GetUserByEmailAsync(string email);
}
