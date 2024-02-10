namespace Kollity.API.Dtos.Auth;

public class TokenDto
{
    public IEnumerable<string> Roles { get; set; }
    public string Token { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string ProfileImage { get; set; }
}