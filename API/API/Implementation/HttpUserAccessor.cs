using System.Security.Claims;
using Application.Abstractions;

namespace API.Implementation;

public class HttpUserAccessor : IUserAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private IEnumerable<Claim> Claims => _httpContextAccessor.HttpContext?.User?.Claims;

    public HttpUserAccessor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetCurrentUserId()
    {
        var id = Claims?.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Sid))?.Value;
        return string.IsNullOrWhiteSpace(id) ? Guid.Empty : Guid.Parse(id);
    }

    public string GetCurrentUserUserName() =>
        Claims?.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))?.Value;

    public List<string> GetCurrentUserRoles() => Claims?
        .Where(c => c.Type.Equals(ClaimTypes.Role))
        .Select(c => c.Value)
        .ToList();
}