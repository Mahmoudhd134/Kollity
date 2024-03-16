using Kollity.Application.Abstractions.Events;

namespace Kollity.Application.IntegrationEvents.Identity;

public record EmailSetEvent(string Email, string Token) : IEvent;