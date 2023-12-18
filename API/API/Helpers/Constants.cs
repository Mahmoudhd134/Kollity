using Microsoft.IdentityModel.Tokens;

namespace API.Helpers;

public static class Constants
{
    public static TokenValidationParameters GetTokenValidationParameters(SecurityKey securityKey)
    {
        return new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidateAudience = false,
            ValidateIssuer = false,
            IssuerSigningKey = securityKey,
            ClockSkew = TimeSpan.Zero
        };
    }
}