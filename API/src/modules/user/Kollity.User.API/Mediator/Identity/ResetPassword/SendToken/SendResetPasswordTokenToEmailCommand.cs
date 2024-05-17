using Kollity.Common.Abstractions.Messages;

namespace Kollity.User.API.Mediator.Identity.ResetPassword.SendToken;

public record SendResetPasswordTokenToEmailCommand(string Email) : ICommand;