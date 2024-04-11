using Kollity.User.API.Abstraction.Messages;

namespace Kollity.User.API.Mediator.Identity.SetEmail.Confirm;

public record ConfirmEmailCommand(string Token) : ICommand;