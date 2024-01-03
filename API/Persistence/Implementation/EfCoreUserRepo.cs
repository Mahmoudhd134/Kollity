using Domain.Identity.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Implementation;

public class EfCoreUserRepo : IUserRepository
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<BaseUser> _userManager;

    public EfCoreUserRepo(ApplicationDbContext context, UserManager<BaseUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<BaseUser> FindByNameAsync(string userName, CancellationToken cancellationToken = default)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == userName.ToUpper(),
            cancellationToken);
    }

    public async Task<bool> CheckPasswordAsync(BaseUser user, string password)
    {
        return await _userManager.CheckPasswordAsync(user, password);
    }

    public async Task<IList<string>> GetRolesAsync(BaseUser user)
    {
        return await _userManager.GetRolesAsync(user);
    }

    public async Task AddToRoleAsync(BaseUser user, string role)
    {
        await _userManager.AddToRoleAsync(user, role);
    }

    public async Task<bool> IsEmailUsed(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Users.AnyAsync(
            u => u.NormalizedEmail == email.ToUpper(), cancellationToken);
    }

    public async Task<bool> IsUserNameUsed(string userName, CancellationToken cancellationToken = default)
    {
        return await _context.Users.AnyAsync(
            u => u.NormalizedUserName == userName.ToUpper(), cancellationToken);
    }
}