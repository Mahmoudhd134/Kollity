using Kollity.Services.Domain.ErrorHandlers.Abstractions;
using Kollity.Services.API.Dtos.Auth;

namespace Kollity.Services.API.Abstractions;

public interface IAuthServices
{
    Task<Result<RefreshTokenDto>> LoginUser(LoginUserDto loginUserDto, string userAgent);
    Task<Result<RefreshTokenDto>> RefreshToken(Guid userId, string oldRefreshToken, string userAgent);
}