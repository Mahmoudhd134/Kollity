using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Kollity.API.Abstractions;
using Kollity.API.Dtos.Auth;
using Kollity.API.Helpers;
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Kollity.Domain.Identity.User;
using Kollity.Domain.Identity.UserRefreshToken;
using Kollity.Persistence.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Kollity.API.Implementation;

public class JwtAuthServices : IAuthServices
{
    private readonly ApplicationDbContext _context;
    private readonly JwtConfiguration _jwtConfiguration;
    private readonly UserManager<BaseUser> _userManager;

    public JwtAuthServices(ApplicationDbContext context,
        UserManager<BaseUser> userManager,
        IOptions<JwtConfiguration> jwtConfiguration)
    {
        _context = context;
        _userManager = userManager;
        _jwtConfiguration = jwtConfiguration.Value;
    }

    public async Task<Result<RefreshTokenDto>> LoginUser(LoginUserDto loginUserDto, string userAgent)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.NormalizedUserName == loginUserDto.Username.ToUpper());

        if (user == null)
            return UserErrors.WrongUsername;
        ;
        if (await _userManager.CheckPasswordAsync(user, loginUserDto.Password) == false)
            return UserErrors.WrongPassword;

        var oldRefreshToken = await _context.UserRefreshTokens
            .FirstOrDefaultAsync(urt => urt.UserId == user.Id && urt.UserAgent == userAgent);

        var result = await GetTokenAndRefreshToken(user);
        if (result.IsSuccess == false)
            return result.Errors;

        if (oldRefreshToken == null)
        {
            await _context.UserRefreshTokens.AddAsync(new UserRefreshToken
            {
                UserId = user.Id,
                RefreshToken = result.Data.RefreshToken,
                UserAgent = userAgent
            });
        }
        else
        {
            oldRefreshToken.RefreshToken = result.Data.RefreshToken;
            _context.UserRefreshTokens.Update(oldRefreshToken);
        }

        await _context.SaveChangesAsync();

        result.Data.UserName = user.UserName;
        result.Data.Email = user.Email;
        result.Data.ProfileImage = user.ProfileImage;
        result.Data.Id = user.Id;
        return result.Data;
    }

    public async Task<Result<RefreshTokenDto>> RefreshToken(Guid userId, string refreshToken, string userAgent)
    {
        if (string.IsNullOrWhiteSpace(refreshToken))
            return UserErrors.ExpireRefreshToken;

        var userRefreshToken = await _context.UserRefreshTokens
            .Include(u => u.User)
            .FirstOrDefaultAsync(u => u.UserId == userId && u.UserAgent == userAgent);

        if (userRefreshToken == null)
            return UserErrors.NotSignedIn;

        if (refreshToken.Equals(userRefreshToken.RefreshToken) == false)
            return UserErrors.WrongRefreshToken;

        var result = await GetTokenAndRefreshToken(userRefreshToken.User);

        if (result.IsSuccess == false)
            return result.Errors;

        var newRefreshTokenDto = new RefreshTokenDto
        {
            UserId = userId.ToString(),
            Roles = result.Data.Roles,
            Token = result.Data.Token,
            RefreshToken = result.Data.RefreshToken,
            UserName = userRefreshToken.User.UserName,
            Email = userRefreshToken.User.Email,
            ProfileImage = userRefreshToken.User.ProfileImage,
            Id = userRefreshToken.UserId,
            Expiry = result.Data.Expiry
        };

        userRefreshToken.RefreshToken = newRefreshTokenDto.RefreshToken;

        await _context.SaveChangesAsync();

        return newRefreshTokenDto;
    }

    private async Task<Result<RefreshTokenDto>> GetTokenAndRefreshToken(BaseUser user)
    {
        var (roles, claims) = await GetRolesAndClaimsAsync(user);
        var refreshToken = GenerateRefreshToken();
        var (token, expiry) = GenerateToken(claims);

        return new RefreshTokenDto
        {
            RefreshToken = refreshToken,
            Token = token,
            Expiry = expiry,
            Roles = roles,
            UserId = user.Id.ToString()
        };
    }

    private async Task<(IEnumerable<string>, IEnumerable<Claim>)> GetRolesAndClaimsAsync(BaseUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        var claims = roles
            .Select(r => new Claim(ClaimTypes.Role, r))
            .Append(new Claim(ClaimTypes.NameIdentifier, user.UserName!))
            .Append(new Claim(ClaimTypes.Sid, user.Id.ToString()));
        return (roles, claims);
    }

    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private (string token, DateTime expiry) GenerateToken(IEnumerable<Claim> claims)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_jwtConfiguration.ExpiryInMinutes),
            // Expires = DateTime.Now.AddSeconds(10),
            SigningCredentials = new SigningCredentials(_jwtConfiguration.SecurityKey, SecurityAlgorithms.HmacSha256)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
        return (tokenHandler.WriteToken(securityToken),
            tokenDescriptor.Expires ?? DateTime.UtcNow.AddMinutes(_jwtConfiguration.ExpiryInMinutes));
    }
}