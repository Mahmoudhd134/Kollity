using Kollity.Common.ErrorHandling;
using Kollity.User.API.Data;
using Kollity.User.API.Models;
using Kollity.User.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kollity.User.API.Services;

public class UserIntegrationServices : IUserIntegrationServices
{
    private readonly UserManager<BaseUser> _userManager;
    private readonly UserDbContext _context;

    public UserIntegrationServices(UserManager<BaseUser> userManager,UserDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<Result> AddUser(Guid id, string username, string email, string password, UserRole userRole)
    {
        var user = new BaseUser()
        {
            Id = id,
            UserName = username,
            Email = email,
        };
        var result = await _userManager.CreateAsync(user, password);
        if (result.Succeeded == false)
            return result.Errors.Select(x => Error.Validation(x.Code, x.Description)).ToList();

        await _userManager.AddToRoleAsync(user, userRole == UserRole.Student ? Role.Student
            : userRole == UserRole.Doctor ? Role.Doctor : Role.Assistant);

        return Result.Success();
    }

    public async Task<Result> EditUser(Guid id, string username)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user is null)
            return UserErrors.IdNotFound(id);
        var nu = username?.ToUpper();
        var found = await _context.Users.AnyAsync(x => x.NormalizedUserName == nu);
        if(found)
            return UserErrors.UserNameAlreadyUsed(username);
        user.UserName = username;
        user.NormalizedUserName = username?.ToUpper();
        await _context.SaveChangesAsync();
        return Result.Success();
    }

    public async Task<Result> DeleteUser(Guid id)
    {
        var r = await _context.Users
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();
        if (r == 0)
            return UserErrors.IdNotFound(id);
        return Result.Success();
    }
}