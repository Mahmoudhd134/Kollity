using Kollity.User.API.Abstraction.Messages;

namespace Kollity.User.API.Mediator.Identity.ResetPassword.SendToken;

public record SendResetPasswordTokenToEmailCommand(string Email) : ICommand;