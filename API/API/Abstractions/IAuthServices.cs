using API.Dtos.Auth;
using Domain.ErrorHandlers;

namespace API.Abstractions;

public interface IAuthServices
{
    Task<Result<RefreshTokenDto>> LoginUser(LoginUserDto loginUserDto, string userAgent);
    Task<Result<RefreshTokenDto>> RefreshToken(Guid userId, string oldRefreshToken, string userAgent);
}