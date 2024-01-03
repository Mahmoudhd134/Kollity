using Domain.Identity.Role;

namespace Domain.Identity.User;

public interface IUserRepository
{
    Task<BaseUser> FindByNameAsync(string userName, CancellationToken cancellationToken = default);
    Task<bool> CheckPasswordAsync(BaseUser user, string password);
    Task<IList<string>> GetRolesAsync(BaseUser user);
    Task AddToRoleAsync(BaseUser user, string role);
    Task<bool> IsEmailUsed(string email, CancellationToken cancellationToken = default);
    Task<bool> IsUserNameUsed(string userName, CancellationToken cancellationToken = default);
}