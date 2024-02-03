namespace Application.Commands.Identity.ResetPassword.SendToken;

public record SendResetPasswordTokenToEmailCommand(string Email) : ICommand;