using Kollity.Application.Abstractions;
using Kollity.Application.Abstractions.Services;
using Kollity.Application.Dtos.Email;
using Kollity.Application.Extensions;
using Kollity.Application.Queries.Identity.IsEmailUsed;
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Kollity.Domain.Identity.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Identity.SetEmail.Set;

public class SetEmailCommandHandler : ICommandHandler<SetEmailCommand>
{
    private readonly IEmailService _emailService;
    private readonly IUserServices _userServices;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<BaseUser> _userManager;

    public SetEmailCommandHandler(UserManager<BaseUser> userManager, IEmailService emailService,
        IUserServices userServices, ApplicationDbContext context)
    {
        _userManager = userManager;
        _emailService = emailService;
        _userServices = userServices;
        _context = context;
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

        var result = await _emailService.TrySendConfirmationEmailAsync(request.Email, token);

        return result ? Result.Success() : Error.UnKnown;
    }
}