using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Auth;

public class LoginUserDto
{
    [Required] public string Username { get; set; }
    [Required] public string Password { get; set; }
}