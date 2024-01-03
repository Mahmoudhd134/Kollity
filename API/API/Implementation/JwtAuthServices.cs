using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using API.Abstractions;
using API.Dtos.Auth;
using API.Helpers;
using Domain.ErrorHandlers;
using Domain.Identity.User;
using Domain.Identity.UserRefreshToken;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Persistence.Abstractions;

namespace API.Implementation;

public class JwtAuthServices : IAuthServices
{
    private readonly IUserRepository _userRepository;
    private readonly IUserRefreshTokenRepository _userRefreshTokenRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly JwtConfiguration _jwtConfiguration;

    public JwtAuthServices(IUserRepository userRepository,
        IUserRefreshTokenRepository userRefreshTokenRepository,
        IUnitOfWork unitOfWork,
        IOptions<JwtConfiguration> jwtConfiguration)
    {
        _userRepository = userRepository;
        _userRefreshTokenRepository = userRefreshTokenRepository;
        _unitOfWork = unitOfWork;
        _jwtConfiguration = jwtConfiguration.Value;
    }

    public async Task<Result<RefreshTokenDto>> LoginUser(LoginUserDto loginUserDto, string userAgent)
    {
        var user = await _userRepository.FindByNameAsync(loginUserDto.Username);
        if (user == null)
            return UserErrors.WrongUsername;

        if (await _userRepository.CheckPasswordAsync(user, loginUserDto.Password) == false)
            return UserErrors.WrongPassword;

        // var oldRefreshToken = await _context.UserRefreshTokens
        //     .FirstOrDefaultAsync(u => u.UserId.Equals(user.Id) && u.UserAgent.Equals(userAgent));
        var oldRefreshToken = await _userRefreshTokenRepository.GetRefreshTokenForUser(user.Id, userAgent);

        var result = await GetTokenAndRefreshToken(user);
        if (result.IsSuccess == false)
            return result.Errors;

        if (oldRefreshToken == null)
        {
            // await _context.UserRefreshTokens.AddAsync(new UserRefreshToken
            // {
            //     UserId = user.Id,
            //     RefreshToken = result.Data.RefreshToken,
            //     UserAgent = userAgent
            // });
            await _userRefreshTokenRepository.AddRefreshTokenWithUserAgentAsync(new UserRefreshToken
            {
                UserId = user.Id,
                RefreshToken = result.Data.RefreshToken,
                UserAgent = userAgent
            });
        }
        else
        {
            oldRefreshToken.RefreshToken = result.Data.RefreshToken;
            // _context.UserRefreshTokens.Update(oldRefreshToken);
            await _userRefreshTokenRepository.Update(oldRefreshToken);
        }

        await _unitOfWork.SaveChangesAsync();

        result.Data.UserName = user.UserName;
        result.Data.Email = user.Email;
        result.Data.ProfileImage = user.ProfileImage;
        return result.Data;
    }

    public async Task<Result<RefreshTokenDto>> RefreshToken(Guid userId, string refreshToken, string userAgent)
    {
        if (string.IsNullOrWhiteSpace(refreshToken))
            return UserErrors.ExpireRefreshToken;

        var userRefreshToken = await _userRefreshTokenRepository.GetRefreshTokenForUserWithUser(userId, userAgent);
        // await _context.UserRefreshTokens
        // .Include(u => u.User)
        // .FirstOrDefaultAsync(u => u.UserId == userId && u.UserAgent == userAgent);

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
            ProfileImage = userRefreshToken.User.ProfileImage
        };

        userRefreshToken.RefreshToken = newRefreshTokenDto.RefreshToken;
        await _userRefreshTokenRepository.Update(userRefreshToken);
        
        await _unitOfWork.SaveChangesAsync();

        return newRefreshTokenDto;
    }

    private async Task<Result<RefreshTokenDto>> GetTokenAndRefreshToken(BaseUser user)
    {
        var (roles, claims) = await GetRolesAndClaimsAsync(user);
        var refreshToken = GenerateRefreshToken();
        var token = GenerateToken(claims);

        return new RefreshTokenDto
        {
            RefreshToken = refreshToken,
            Token = token,
            Roles = roles,
            UserId = user.Id.ToString()
        };
    }

    private async Task<(IEnumerable<string>, IEnumerable<Claim>)> GetRolesAndClaimsAsync(BaseUser user)
    {
        var roles = await _userRepository.GetRolesAsync(user);
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

    private string GenerateToken(IEnumerable<Claim> claims)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_jwtConfiguration.ExpiryInMinutes),
            // Expires = DateTime.Now.AddSeconds(5),
            SigningCredentials = new SigningCredentials(_jwtConfiguration.SecurityKey, SecurityAlgorithms.HmacSha256)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
        return tokenHandler.WriteToken(securityToken);
    }
}