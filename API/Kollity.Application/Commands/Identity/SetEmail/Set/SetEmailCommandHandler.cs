using Kollity.Application.Abstractions;
using Kollity.Application.Abstractions.Events;
using Kollity.Application.Extensions;
using Kollity.Contracts.Events.Identity;
using Kollity.Domain.Identity.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Identity.SetEmail.Set;

public class SetEmailCommandHandler : ICommandHandler<SetEmailCommand>
{
    private readonly IUserServices _userServices;
    private readonly ApplicationDbContext _context;
    private readonly EventCollection _eventCollection;
    private readonly UserManager<BaseUser> _userManager;

    public SetEmailCommandHandler(UserManager<BaseUser> userManager, IUserServices userServices,
        ApplicationDbContext context, EventCollection eventCollection)
    {
        _userManager = userManager;
        _userServices = userServices;
        _context = context;
        _eventCollection = eventCollection;
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
            return setResult.Errors.ToAppError().ToList();

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);


        _eventCollection.Raise(new EmailSetEvent(request.Email, token));
        // var result = await _emailService.TrySendConfirmationEmailAsync(request.Email, token);

        // return result ? Result.Success() : Error.UnKnown;
        return Result.Success();
    }
}