namespace Kollity.Services.Contracts.Room;

public class RoomContentAddedIntegrationEvent
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid RoomId { get; set; }
    public DateTime UploadTime { get; set; }
    public Guid UploaderId { get; set; }
}