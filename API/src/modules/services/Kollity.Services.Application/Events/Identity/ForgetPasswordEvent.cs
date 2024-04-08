using Kollity.Services.Application.Abstractions.Events;

namespace Kollity.Services.Application.Events.Identity;

public record ForgetPasswordEvent(string Email, string Token) : IEvent;