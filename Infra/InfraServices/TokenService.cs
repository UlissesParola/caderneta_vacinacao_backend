using Core.Entities;
using Core.Interfaces.InfraServices;
using Core.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infra.InfraServices;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Result<string> GenerateToken(Usuario user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Issuer"],
            new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            },
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds
        );

        return Result<string>.Success(new JwtSecurityTokenHandler().WriteToken(token));
    }

    public Result<ClaimsPrincipal?> ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

        try
        {
            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = _configuration["Jwt:Issuer"],
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            if (validatedToken is JwtSecurityToken jwtToken &&
                jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                return Result<ClaimsPrincipal?>.Success(principal); // Token válido
            }
            else
            {
                return Result<ClaimsPrincipal?>.Failure("Invalid token algorithm.", 400);
            }
        }
        catch (SecurityTokenExpiredException)
        {
            return Result<ClaimsPrincipal?>.Failure("Token expired.", 401);
        }
        catch (SecurityTokenException)
        {
            return Result<ClaimsPrincipal?>.Failure("Invalid token.", 400);
        }
        catch (Exception ex)
        {
            return Result<ClaimsPrincipal?>.Failure($"An error occurred during token validation: {ex.Message}", 500);
        }
    }
}