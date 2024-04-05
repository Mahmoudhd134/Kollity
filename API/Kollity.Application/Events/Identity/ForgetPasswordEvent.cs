using Kollity.Application.Abstractions.Events;

namespace Kollity.Application.Events.Identity;

public record ForgetPasswordEvent(string Email, string Token) : IEvent;