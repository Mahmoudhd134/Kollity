using Kollity.Application.Dtos.Identity;

namespace Kollity.Application.Commands.Identity.ResetPassword.Reset;

public record ResetPasswordCommand(ResetPasswordDto ResetPasswordDto) : ICommand;