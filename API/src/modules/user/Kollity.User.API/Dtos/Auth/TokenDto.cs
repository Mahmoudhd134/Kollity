namespace Kollity.User.API.Dtos.Auth;

public class TokenDto
{
    public IEnumerable<string> Roles { get; set; }
    public Guid Id { get; set; }
    public string Token { get; set; }
    public DateTime Expiry { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string ProfileImage { get; set; }
}