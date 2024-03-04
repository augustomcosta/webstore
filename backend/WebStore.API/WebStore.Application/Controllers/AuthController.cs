using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebStore.API.Interfaces;
using WebStore.Domain.Entities.Authentication;
using WebStore.Domain.Entities.Identity;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace WebStore.API.Controllers;

[Route("api/{controller}")]
[ApiController]
public class AuthController : Controller
{
    private readonly ITokenService _tokenService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _config;

    public AuthController(ITokenService tokenService, UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager, IConfiguration config)
    {
        _tokenService = tokenService;
        _userManager = userManager;
        _roleManager = roleManager;
        _config = config;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody]LoginModel model)
    {
        var user = await _userManager.FindByNameAsync(model.Name!);

        if (user is not null && await _userManager.CheckPasswordAsync(user, model.Password!))
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email,user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            authClaims.AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));

            var token = _tokenService.GenerateAccessToken(authClaims, _config);
            var refreshToken = _tokenService.GenerateRefreshToken();

            _ = int.TryParse(_config["JWT:RefreshTokenValidityInMinutes"], out var refreshTokenValidityInMinutes);

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(refreshTokenValidityInMinutes);
            await _userManager.UpdateAsync(user);

            return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                Expiration = token.ValidTo
            });
        }
        return Unauthorized();
    }

    [HttpPost]
    [Route("register-admin")]
    public async Task<IActionResult> RegisterAdmin([FromBody]RegisterModel registerModel)
    {
        var userExists = _userManager.FindByNameAsync(registerModel.Username!);
        if (userExists is not null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "User already exists");
        }

        ApplicationUser user = new()
        {
            Email = registerModel.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = registerModel.Username
        };
        var result = await _userManager.CreateAsync(user, registerModel.Password!);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error while creating User");
        }

        if (!await _roleManager.RoleExistsAsync(UserRoles.Admin!))
        {
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin!));
        }

        if (!await _roleManager.RoleExistsAsync(UserRoles.User!))
        {
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.User!));
        }
        
        if(await _roleManager.RoleExistsAsync(UserRoles.Admin!))
        {
            await _userManager.AddToRoleAsync(user, UserRoles.Admin!);
        }

        if (await _roleManager.RoleExistsAsync(UserRoles.Admin!))
        {
            await _userManager.AddToRoleAsync(user,UserRoles.User!);
        }
        
        return Ok(StatusCode(StatusCodes.Status201Created,"User created sucessfully"));
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody]RegisterModel registerModel)
    {
        var userExists = await _userManager.FindByNameAsync(registerModel.Username!);

        if (userExists is not null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "User already exists");
        }

        ApplicationUser user = new()
        {
            Email = registerModel.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = registerModel.Username
        };
        
        var result = await _userManager.CreateAsync(user,registerModel.Password!);
        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error while creating user");
        }
        
        return Ok(StatusCode(StatusCodes.Status201Created, "User created sucessfully"));
    }

    [HttpPost]
    [Route("refresh-token")]
    public async Task<IActionResult> RefreshToken(TokenModel? tokenModel)
    {
        if (tokenModel is null)
        {
            return BadRequest("Invalid client request");
        }

        var accessToken = tokenModel.AccessToken;
        var refreshToken = tokenModel.RefreshToken;

        var principal = GetPrincipalFromExpiredToken(accessToken);
        if (principal == null)
        {
            return BadRequest("Invalid access/refresh token");
        }

        var username = principal.Identity!.Name;
        var user = await _userManager.FindByNameAsync(username!);
        //TODO: Finish auth controller
        
        
        return Ok();
    }


    private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SecretKey"])),
            ValidateLifetime = false
        };
        var tokenHandler = new JwtSecurityTokenHandler();

        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg
                .Equals(SecurityAlgorithms.HmacSha256,StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }
        return principal;
    }
}