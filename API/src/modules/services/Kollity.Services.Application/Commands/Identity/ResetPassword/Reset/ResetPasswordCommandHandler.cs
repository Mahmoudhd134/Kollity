// using Kollity.Services.Application.Extensions;
// using Kollity.Services.Domain.Errors;
// using Kollity.Services.Domain.Identity;
// using Microsoft.AspNetCore.Identity;
//
// namespace Kollity.Services.Application.Commands.Identity.ResetPassword.Reset;
//
// public class ResetPasswordCommandHandler : ICommandHandler<ResetPasswordCommand>
// {
//     private readonly UserManager<BaseUser> _userManager;
//
//     public ResetPasswordCommandHandler(UserManager<BaseUser> userManager)
//     {
//         _userManager = userManager;
//     }
//
//     public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
//     {
//         var user = await _userManager.FindByEmailAsync(request.ResetPasswordDto.Email);
//         if (user is null)
//             return UserErrors.EmailNotFound(request.ResetPasswordDto.Email);
//
//         var result = await _userManager.ResetPasswordAsync(user, request.ResetPasswordDto.Token,
//             request.ResetPasswordDto.NewPassword);
//
//         return result.Succeeded ? Result.Success() : result.Errors.ToAppError().ToList();
//     }
// }