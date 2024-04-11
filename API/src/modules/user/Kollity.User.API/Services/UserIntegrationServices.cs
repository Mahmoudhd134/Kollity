using Kollity.Common.ErrorHandling;
using Kollity.User.API.Models;
using Kollity.User.Contracts;
using Microsoft.AspNetCore.Identity;

namespace Kollity.User.API.Services;

public class UserIntegrationServices : IUserIntegrationServices
{
    private readonly UserManager<BaseUser> _userManager;

    public UserIntegrationServices(UserManager<BaseUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result> AddUser(string username, string email, string password, UserRole userRole)
    {
        var user = new BaseUser()
        {
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
}