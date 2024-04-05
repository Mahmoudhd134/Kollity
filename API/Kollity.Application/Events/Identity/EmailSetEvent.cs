using Kollity.Application.Abstractions.Events;

namespace Kollity.Application.Events.Identity;

public record EmailSetEvent(string Email, string Token) : IEvent;