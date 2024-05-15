namespace Kollity.User.API.Abstraction.Services;

public interface IUserServices
{
    Guid GetCurrentUserId();
    string GetCurrentUserUserName();
    List<string> GetCurrentUserRoles();
    bool IsInRole(string role);
}