using Microsoft.AspNetCore.Identity;

namespace Domain.Identity.User;

public class BaseUser : IdentityUser<Guid>
{
    public string ProfileImage { get; set; }
    public string Type { get; set; }
    public List<UserRefreshToken.UserRefreshToken> UserRefreshTokens { get; set; }
}