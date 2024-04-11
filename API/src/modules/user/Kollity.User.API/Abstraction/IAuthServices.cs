using Kollity.Common.ErrorHandling;
using Kollity.User.API.Dtos.Auth;

namespace Kollity.User.API.Abstraction;

public interface IAuthServices
{
    Task<Result<RefreshTokenDto>> LoginUser(LoginUserDto loginUserDto, string deviceToken);
    Task<Result<RefreshTokenDto>> RefreshToken(Guid userId, string oldRefreshToken, string deviceToken);
}