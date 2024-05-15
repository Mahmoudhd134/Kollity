using Microsoft.AspNetCore.Identity;

namespace Kollity.User.API.Models;

public class BaseUser : IdentityUser<Guid>
{
    public string ProfileImage { get; set; }
    public ICollection<UserRefreshToken> UserRefreshTokens { get; set; } = [];
    public ICollection<IdentityUserRole<Guid>> Roles { get; set; } = [];
}