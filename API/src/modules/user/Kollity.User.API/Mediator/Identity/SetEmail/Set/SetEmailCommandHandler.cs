using Kollity.Common.ErrorHandling;
using Kollity.User.API.Abstraction;
using Kollity.User.API.Abstraction.Messages;
using Kollity.User.API.Abstraction.Services;
using Kollity.User.API.Data;
using Kollity.User.API.Events.Identity;
using Kollity.User.API.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kollity.User.API.Mediator.Identity.SetEmail.Set;

public class SetEmailCommandHandler : ICommandHandler<SetEmailCommand>
{
    private readonly IUserServices _userServices;
    private readonly UserDbContext _context;
    private readonly IPublisher _publisher;
    private readonly UserManager<BaseUser> _userManager;

    public SetEmailCommandHandler(UserManager<BaseUser> userManager, IUserServices userServices,
        UserDbContext context, IPublisher publisher)
    {
        _userManager = userManager;
        _userServices = userServices;
        _context = context;
        _publisher = publisher;
    }

    public async Task<Result> Handle(SetEmailCommand request, CancellationToken cancellationToken)
    {
        var id = _userServices.GetCurrentUserId();
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user is null)
            return UserErrors.IdNotFound(id);

        var emailInUpper = request.Email.ToUpper();
        var isEmailUsed = await _context.Users
            .Where(x => x.Id != id)
            .AnyAsync(x => x.NormalizedEmail == emailInUpper, cancellationToken);
        if (isEmailUsed)
            return UserErrors.EmailAlreadyUsed(request.Email);

        var setResult = await _userManager.SetEmailAsync(user, request.Email);
        if (setResult.Succeeded == false)
            return setResult.Errors.Select(x => Error.Validation(x.Code, x.Description)).ToList();

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        await _publisher.Publish(new EmailSetEvent(request.Email, token), cancellationToken);

        return Result.Success();
    }
}