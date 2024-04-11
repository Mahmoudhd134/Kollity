using Kollity.Common.ErrorHandling;
using Kollity.User.API.Abstraction;
using Kollity.User.API.Dtos.Auth;
using Kollity.User.API.Extensions;
using Kollity.User.API.Helpers;
using Kollity.User.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace Kollity.User.API.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IAuthServices _authServices;
    private readonly JwtConfiguration _jwtConfiguration;

    public UserController(IOptions<JwtConfiguration> jwtConfiguration, IAuthServices authServices)
    {
        _jwtConfiguration = jwtConfiguration.Value;
        _authServices = authServices;
    }

    [HttpPost("login")]
    [SwaggerResponse(200, type: typeof(TokenDto))]
    public async Task<IResult> Login([FromBody] LoginUserDto loginUserDto, string deviceToken)
    {
        var result = await _authServices.LoginUser(loginUserDto, deviceToken ?? Request.Headers.UserAgent);
        if (result.IsSuccess == false)
            return result.ToIResult();

        SetToHttpCookie(result.Data);
        var tokenDto = ToTokenDto(result.Data);
        return Results.Ok(tokenDto);
    }

    [HttpPost("refresh-token")]
    [SwaggerResponse(200, type: typeof(TokenDto))]
    public async Task<IResult> RefreshToken(string deviceToken)
    {
        if (Request.Cookies.TryGetValue("id", out var id) == false)
            return Result.Failure(UserErrors.ExpireRefreshToken).ToIResult();
        if (string.IsNullOrWhiteSpace(id))
            return Result.Failure(UserErrors.ExpireRefreshToken).ToIResult();

        if (Request.Cookies.TryGetValue("refreshToken", out var refreshToken) == false)
            return Result.Failure(UserErrors.ExpireRefreshToken).ToIResult();
        if (string.IsNullOrWhiteSpace(refreshToken))
            return Result.Failure(UserErrors.ExpireRefreshToken).ToIResult();

        var result =
            await _authServices.RefreshToken(Guid.Parse(id), refreshToken, deviceToken ?? Request.Headers.UserAgent);

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