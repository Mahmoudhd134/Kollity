using Kollity.Domain.Identity.User;

namespace Kollity.Domain.Identity.UserRefreshToken;

public class UserRefreshToken
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public BaseUser User { get; set; }
    public string UserAgent { get; set; }
    public string RefreshToken { get; set; }
}