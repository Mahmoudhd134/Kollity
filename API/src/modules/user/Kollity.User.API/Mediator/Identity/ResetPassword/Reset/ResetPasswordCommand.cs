using Kollity.User.API.Abstraction.Messages;
using Kollity.User.API.Dtos.Identity;

namespace Kollity.User.API.Mediator.Identity.ResetPassword.Reset;

public record ResetPasswordCommand(ResetPasswordDto ResetPasswordDto) : ICommand;