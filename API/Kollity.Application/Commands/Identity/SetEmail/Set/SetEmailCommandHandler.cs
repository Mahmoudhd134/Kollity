using Kollity.Application.Abstractions;
<<<<<<< HEAD
using Kollity.Application.Dtos.Email;
using Kollity.Application.Extensions;
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Kollity.Domain.Identity.User;
using Microsoft.AspNetCore.Identity;
=======
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
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e

namespace Kollity.Application.Commands.Identity.SetEmail.Set;

public class SetEmailCommandHandler : ICommandHandler<SetEmailCommand>
{
    private readonly IEmailService _emailService;
<<<<<<< HEAD
    private readonly IUserAccessor _userAccessor;
    private readonly UserManager<BaseUser> _userManager;

    public SetEmailCommandHandler(UserManager<BaseUser> userManager, IEmailService emailService,
        IUserAccessor userAccessor)
    {
        _userManager = userManager;
        _emailService = emailService;
        _userAccessor = userAccessor;
=======
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
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
    }

    public async Task<Result> Handle(SetEmailCommand request, CancellationToken cancellationToken)
    {
<<<<<<< HEAD
        var id = _userAccessor.GetCurrentUserId();
=======
        var id = _userServices.GetCurrentUserId();
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user is null)
            return UserErrors.IdNotFound(id);

<<<<<<< HEAD
=======
        var emailInUpper = request.Email.ToUpper();
        var isEmailUsed = await _context.Users
            .Where(x => x.Id != id)
            .AnyAsync(x => x.NormalizedEmail == emailInUpper, cancellationToken);
        if (isEmailUsed)
            return UserErrors.EmailAlreadyUsed(request.Email);

>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
        var setResult = await _userManager.SetEmailAsync(user, request.Email);
        if (setResult.Succeeded == false)
            return setResult.Errors.ToAppError().ToList();

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

<<<<<<< HEAD
        var result = await _emailService.TrySendAsync(new EmailData
        {
            Subject = "Reset Password",
            ToEmail = user.Email,
            HtmlBody = $"""
                        <div  style="text-align:center;">
                        <h3>
                        Confirm Email
                        </h3>
                        <p>
                        click the button below to confirm your email
                        </p>
                        <a href="http://localhost:5196/api/identity/confirm-email?email={request.Email}&token={token}
                        Confirm Email
                        </a>
                        </div>
                        """
        });
=======
        var result = await _emailService.TrySendConfirmationEmailAsync(request.Email, token);
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e

        return result ? Result.Success() : Error.UnKnown;
    }
}