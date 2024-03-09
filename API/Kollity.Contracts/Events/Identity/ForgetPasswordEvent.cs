namespace Kollity.Contracts.Events.Identity;

public record ForgetPasswordEvent(string Email, string Token) : IEvent;