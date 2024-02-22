using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebStore.API.Interfaces;

namespace WebStore.API.Services;

public class TokenService : ITokenService
{
    
    public JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims, IConfiguration _config)
    {
        throw new NotImplementedException();
    }

    public string GenerateRefreshToken()
    {
        throw new NotImplementedException();
    }

    public ClaimsPrincipal getPrincipalFromExpiredToken(string token, IConfiguration _config)
    {
        throw new NotImplementedException();
    }
}