using Application.Dtos.Identity;

namespace Application.Commands.Identity.ResetPassword.Reset;

public record ResetPasswordCommand(ResetPasswordDto ResetPasswordDto) : ICommand;