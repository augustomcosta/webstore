using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebStore.API.Interfaces;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Authentication;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.Repositories;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace WebStore.API.Controllers;

[EnableCors("AllowClient")]
[Route("api/[controller]")]
[ApiController]
public class AuthController : Controller
{
    private readonly IConfiguration _config;
    private readonly ILogger<AuthController> _logger;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ITokenService _tokenService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserRepository _userRepo;

    public AuthController(ITokenService tokenService, UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager, IConfiguration config, ILogger<AuthController> logger,
        IUserService userService,
        IUserRepository userRepo)
    {
        _tokenService = tokenService;
        _userManager = userManager;
        _roleManager = roleManager;
        _config = config;
        _logger = logger;
        _userRepo = userRepo;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _userManager.FindByNameAsync(model.UserName!);

        if (user is not null && await _userManager.CheckPasswordAsync(user, model.Password!))
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
                new(ClaimTypes.Name, user.UserName!),
                new(ClaimTypes.Email, user.Email!),
                new("id", user.UserName!),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            authClaims.AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));

            var token = _tokenService.GenerateAccessToken(authClaims, _config);
            var refreshToken = _tokenService.GenerateRefreshToken();

            _ = int.TryParse(_config["JWT:RefreshTokenValidityInMinutes"], out var refreshTokenValidityInMinutes);

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(refreshTokenValidityInMinutes);
            const bool isSuccess = true;
            await _userManager.UpdateAsync(user);
            
            var userModel = await _userRepo.GetById(user.Id);
            var name = userModel.FirstName;

            return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                Expiration = token.ValidTo.ToString(CultureInfo.CurrentCulture),
                IsSuccess = isSuccess,
                loggedUser = model.UserName,
                userName = name,
                userId = userModel.Id,
                wishlistId = userModel.WishlistId
            });
        }

        return Unauthorized();
    }

    [DisableCors]
    [HttpPost]
    [Route("register-admin")]
    public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel registerModel)
    {
        var userExists = _userManager.FindByNameAsync(registerModel.Username!);
        if (userExists is not null) return StatusCode(StatusCodes.Status500InternalServerError, "User already exists");

        ApplicationUser user = new()
        {
            Email = registerModel.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = registerModel.Username
        };
        var result = await _userManager.CreateAsync(user, registerModel.Password!);

        if (!result.Succeeded) return StatusCode(StatusCodes.Status500InternalServerError, "Error while creating User");

        if (!await _roleManager.RoleExistsAsync(UserRoles.Admin!))
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin!));

        if (!await _roleManager.RoleExistsAsync(UserRoles.User!))
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.User!));

        if (await _roleManager.RoleExistsAsync(UserRoles.Admin!))
            await _userManager.AddToRoleAsync(user, UserRoles.Admin!);

        if (await _roleManager.RoleExistsAsync(UserRoles.Admin!))
            await _userManager.AddToRoleAsync(user, UserRoles.User!);

        return Ok(StatusCode(StatusCodes.Status201Created, "User created sucessfully"));
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] UserRegistrationModel model)
    {
        var userExists = await _userManager.FindByNameAsync(model.Username!);

        if (userExists is not null) return StatusCode(StatusCodes.Status500InternalServerError, "User already exists");

        User userEntity = new()
        {
            Username = model.Username!,
            Email = model.Email!,
            Password = model.Password!,
            Cpf = model.Cpf!,
            FirstName = model.FirstName!,
            LastName = model.LastName!
        };

        ApplicationUser user = new()
        {
            Id = userEntity.Id,
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.Username
        };
        
        const bool isSuccess = true;

        await _userRepo.Create(userEntity);

        var result = await _userManager.CreateAsync(user, model.Password!);
        if (!result.Succeeded) return StatusCode(StatusCodes.Status500InternalServerError, "Error while creating user");

        return Ok(new
        {
            IsSuccess = isSuccess
        });
    }

    [HttpPost]
    [Route("refresh-token")]
    public async Task<IActionResult> RefreshToken(TokenModel? tokenModel)
    {
        if (tokenModel is null) return BadRequest("Invalid client request");

        var accessToken = tokenModel.AccessToken ?? throw new ArgumentNullException(nameof(tokenModel));
        var refreshToken = tokenModel.RefreshToken ?? throw new ArgumentNullException(nameof(tokenModel));

        var principal = GetPrincipalFromExpiredToken(accessToken);
        if (principal == null) return BadRequest("Invalid access/refresh token");

        var username = principal.Identity!.Name;
        var user = await _userManager.FindByNameAsync(username!);
        if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            return BadRequest("Invalid access/refresh token");

        var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims.ToList(), _config);
        var newRefreshToken = _tokenService.GenerateRefreshToken();
        user.RefreshToken = newRefreshToken;

        return new ObjectResult(new
        {
            accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            refreshToken = newRefreshToken
        });
    }

    [Authorize("SuperAdminOnly")]
    [HttpPost]
    [Route("revoke/{username}")]
    public async Task<IActionResult> Revoke(string username)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user == null) return BadRequest("Invalid username");

        user.RefreshToken = null;
        return NoContent();
    }

    [Authorize("ExclusiveOnly")]
    [HttpPost]
    [Route("revoke-all")]
    public async Task<IActionResult> RevokeAll()
    {
        var users = _userManager.Users.ToList();
        foreach (var user in users)
        {
            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);
        }

        return NoContent();
    }

    [HttpPost]
    [Route("CreateRole")]
    public async Task<IActionResult> CreateRole(string roleName)
    {
        var roleExists = await _roleManager.RoleExistsAsync(roleName);
        if (!roleExists)
        {
            var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));

            if (roleResult.Succeeded)
            {
                _logger.LogInformation(1, "Roles Added");
                return StatusCode(StatusCodes.Status200OK, $"Success. Role {roleName} added.");
            }

            _logger.LogInformation(2, "Error");
            return StatusCode(StatusCodes.Status400BadRequest, $"Error.Issue adding the new {roleName} role");
        }

        return StatusCode(StatusCodes.Status400BadRequest, "Error. Role already exists");
    }


    [HttpPost]
    [Route("AddUserToRole")]
    public async Task<IActionResult> AddUserToRole(string email, string roleName)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user != null)
        {
            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                _logger.LogInformation(1, $"User {user.Email} added to role {roleName}");
                return StatusCode(StatusCodes.Status200OK, $"Success. User {user.Email} added to role {roleName}");
            }

            _logger.LogInformation($"Unable to add user {user.Email} to role {roleName}");
            return StatusCode(StatusCodes.Status400BadRequest,
                $"Error. Unable to add user {user.Email} to role {roleName}");
        }

        return BadRequest(new { error = "Unable to find user" });
    }


    private JwtSecurityToken CreateToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SecretKey"]!));
        _ = int.TryParse(_config["JWT:TokenValidityInMinutes"], out var tokenValidityInMinutes);

        var token = new JwtSecurityToken(
            _config["JWT:ValidIssuer"],
            _config["JWT:ValidAudience"],
            expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );
        return token;
    }

    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SecretKey"]!)),
            ValidateLifetime = false
        };
        var tokenHandler = new JwtSecurityTokenHandler();

        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg
                .Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");
        return principal;
    }
}