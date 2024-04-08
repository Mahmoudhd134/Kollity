using Kollity.Services.Application.Abstractions.Messages;

namespace Kollity.Services.Application.Commands.Identity.SetEmail.Confirm;

public record ConfirmEmailCommand(string Token) : ICommand;