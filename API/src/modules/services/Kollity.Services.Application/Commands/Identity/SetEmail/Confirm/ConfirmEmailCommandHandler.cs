// using Kollity.Services.Application.Extensions;
// using Kollity.Services.Domain.Errors;
// using Kollity.Services.Domain.Identity;
// using Microsoft.AspNetCore.Identity;
//
// namespace Kollity.Services.Application.Commands.Identity.SetEmail.Confirm;
//
// public class ConfirmEmailCommandHandler : ICommandHandler<ConfirmEmailCommand>
// {
//     private readonly IUserServices _userServices;
//     private readonly UserManager<BaseUser> _userManager;
//
//     public ConfirmEmailCommandHandler(UserManager<BaseUser> userManager, IUserServices userServices)
//     {
//         _userManager = userManager;
//         _userServices = userServices;
//     }
//
//     public async Task<Result> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
//     {
//         var id = _userServices.GetCurrentUserId();
//         var user = await _userManager.FindByIdAsync(id.ToString());
//         if (user is null)
//             return UserErrors.IdNotFound(id);
//
//         var result = await _userManager.ConfirmEmailAsync(user, request.Token);
//
//         return result.Succeeded ? Result.Success() : result.Errors.ToAppError().ToList();
//     }
// }