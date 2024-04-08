using Kollity.Services.API.Extensions;
using Kollity.Services.Domain.ErrorHandlers.Abstractions;
using Kollity.Services.Domain.ErrorHandlers.Errors;
using Kollity.Services.API.Abstractions;
using Kollity.Services.API.Dtos.Auth;
using Kollity.Services.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace Kollity.Services.API.Controllers;

[AllowAnonymous]
public class AuthController : BaseController
{
    private readonly IAuthServices _authServices;
    private readonly JwtConfiguration _jwtConfiguration;

    public AuthController(IOptions<JwtConfiguration> jwtConfiguration, IAuthServices authServices)
    {
        _jwtConfiguration = jwtConfiguration.Value;
        _authServices = authServices;
    }

    [HttpPost("login")]
    [SwaggerResponse(200, type: typeof(TokenDto))]
    public async Task<IResult> Login([FromBody] LoginUserDto loginUserDto)
    {
        var result = await _authServices.LoginUser(loginUserDto, UserAgent);
        if (result.IsSuccess == false)
            return result.ToIResult();

        SetToHttpCookie(result.Data);
        var tokenDto = ToTokenDto(result.Data);
        return Results.Ok(tokenDto);
    }

    [HttpPost("refresh-token")]
    [SwaggerResponse(200, type: typeof(TokenDto))]
    public async Task<IResult> RefreshToken()
    {
        if (Request.Cookies.TryGetValue("id", out var id) == false)
            return Result.Failure(UserErrors.ExpireRefreshToken).ToIResult();
        if (string.IsNullOrWhiteSpace(id))
            return Result.Failure(UserErrors.ExpireRefreshToken).ToIResult();

        if (Request.Cookies.TryGetValue("refreshToken", out var refreshToken) == false)
            return Result.Failure(UserErrors.ExpireRefreshToken).ToIResult();
        if (string.IsNullOrWhiteSpace(refreshToken))
            return Result.Failure(UserErrors.ExpireRefreshToken).ToIResult();

        var result = await _authServices.RefreshToken(Guid.Parse(id), refreshToken, UserAgent);

        if (result.IsSuccess == false)
            return result.ToIResult();

        SetToHttpCookie(result.Data);
        var tokenDto = ToTokenDto(result.Data);
        return Results.Ok(tokenDto);
    }

    [HttpDelete("logout")]
    public IResult Logout()
    {
        Response.Cookies.Delete("id");
        Response.Cookies.Delete("refreshToken");
        return Results.Empty;
    }

    private TokenDto ToTokenDto(TokenDto tokenDto)
    {
        return new TokenDto
        {
            Token = tokenDto.Token,
            Expiry = tokenDto.Expiry,
            Id = tokenDto.Id,
            Email = tokenDto.Email,
            Roles = tokenDto.Roles,
            UserName = tokenDto.UserName,
            ProfileImage = tokenDto.ProfileImage
        };
    }

    private void SetToHttpCookie(RefreshTokenDto refreshTokenDto)
    {
        var cookiesOption = new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.None,
            Expires = DateTime.UtcNow.AddDays(_jwtConfiguration.RefreshTokenExpiryInDays),
            // Expires = DateTime.Now.AddSeconds(20),
            Secure = true
        };

        Response.Cookies.Append("refreshToken", refreshTokenDto.RefreshToken, cookiesOption);
        Response.Cookies.Append("id", refreshTokenDto.UserId, cookiesOption);
    }
}