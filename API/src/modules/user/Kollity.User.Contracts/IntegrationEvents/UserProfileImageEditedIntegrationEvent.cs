namespace Kollity.User.Contracts.IntegrationEvents;

public class UserProfileImageEditedIntegrationEvent
{
    public Guid Id { get; set; }
    public string ProfileImage { get; set; }
}