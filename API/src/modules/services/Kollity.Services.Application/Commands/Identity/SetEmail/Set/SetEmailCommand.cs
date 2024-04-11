namespace Kollity.Services.Application.Commands.Identity.SetEmail.Set;

public record SetEmailCommand(string Email) : ICommand;