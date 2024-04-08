namespace Kollity.Services.Contracts.Room;

public class RoomAddedIntegrationEvent
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid CourseId { get; set; }
    public Guid DoctorId { get; set; }
    public bool EnsureJoinRequest { get; set; }
}