using Kollity.User.API.Abstraction.Events;

namespace Kollity.User.API.Events.Identity;

public record EmailSetEvent(string Email, string Token) : IEvent;