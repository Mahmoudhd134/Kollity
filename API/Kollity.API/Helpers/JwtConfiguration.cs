using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Kollity.API.Helpers;

public class JwtConfiguration
{
    public string Key { get; set; }
    public double ExpiryInMinutes { get; set; }
    public double RefreshTokenExpiryInDays { get; set; }
    public byte[] KeyBytes => Encoding.UTF8.GetBytes(Key);
    public SecurityKey SecurityKey => new SymmetricSecurityKey(KeyBytes);
}