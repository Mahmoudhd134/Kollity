namespace Kollity.Services.Application.Commands.Identity.SetEmail.Confirm;

public record ConfirmEmailCommand(string Token) : ICommand;