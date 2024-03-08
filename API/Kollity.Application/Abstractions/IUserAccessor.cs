namespace Kollity.Application.Abstractions;

public interface IUserAccessor
{
    Guid GetCurrentUserId();
    string GetCurrentUserUserName();
    List<string> GetCurrentUserRoles();
    bool IsInRole(string role);
}