namespace Kollity.User.API.Models;

public class UserRefreshToken
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public BaseUser User { get; set; }
    public string DeviceToken { get; set; }
    public string RefreshToken { get; set; }
}