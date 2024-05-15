namespace Kollity.User.Contracts.IntegrationEvents;

public class UserEmailEditedIntegrationEvent
{
    public Guid Id { get; set; }
    public string Email { get; set; }
}