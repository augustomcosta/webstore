using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WebStore.API.Interfaces;

public interface ITokenService
{
    public JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims, IConfiguration _config);
    public string GenerateRefreshToken();
    public ClaimsPrincipal getPrincipalFromExpiredToken(string token, IConfiguration _config);
}