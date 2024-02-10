using Kollity.API.Dtos.Auth;
using Kollity.Domain.ErrorHandlers;

namespace Kollity.API.Abstractions;

public interface IAuthServices
{
    Task<Result<RefreshTokenDto>> LoginUser(LoginUserDto loginUserDto, string userAgent);
    Task<Result<RefreshTokenDto>> RefreshToken(Guid userId, string oldRefreshToken, string userAgent);
}