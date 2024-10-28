using Core.Entities;
using Core.Utils;
using System.Security.Claims;

namespace Core.Interfaces.InfraServices;

public interface ITokenService
{
    Result<string> GenerateToken(Usuario user);
    Result<ClaimsPrincipal> ValidateToken(string token);
}
