using Kollity.Common.Abstractions.Messages;

namespace Kollity.User.API.Mediator.Identity.SetEmail.Set;

public record SetEmailCommand(string Email) : ICommand;