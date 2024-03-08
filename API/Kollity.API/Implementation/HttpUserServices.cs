using System.Security.Claims;
using Kollity.Application.Abstractions;
using Kollity.Application.Abstractions.Services;

namespace Kollity.API.Implementation;

public class HttpUserServices : IUserServices
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpUserServices(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private IEnumerable<Claim> Claims => _httpContextAccessor.HttpContext?.User?.Claims;

    public Guid GetCurrentUserId()
    {
        var id = Claims?.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Sid))?.Value;
        return string.IsNullOrWhiteSpace(id) ? Guid.Empty : Guid.Parse(id);
    }

    public string GetCurrentUserUserName()
    {
        return Claims?.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
    }

    public List<string> GetCurrentUserRoles()
    {
        return Claims?
            .Where(c => c.Type.Equals(ClaimTypes.Role))
            .Select(c => c.Value)
            .ToList();
    }

    public bool IsInRole(string role)
    {
        return GetCurrentUserRoles()?.Contains(role) ?? false;
    }
}