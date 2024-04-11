using Kollity.User.API.Abstraction.Messages;

namespace Kollity.User.API.Mediator.Identity.SetEmail.Set;

public record SetEmailCommand(string Email) : ICommand;