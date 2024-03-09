namespace Kollity.Contracts.Events.Identity;

public record EmailSetEvent(string Email, string Token) : IEvent;