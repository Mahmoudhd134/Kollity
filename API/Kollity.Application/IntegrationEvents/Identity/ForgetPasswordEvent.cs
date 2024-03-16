using Kollity.Application.Abstractions.Events;

namespace Kollity.Application.IntegrationEvents.Identity;

public record ForgetPasswordEvent(string Email, string Token) : IEvent;