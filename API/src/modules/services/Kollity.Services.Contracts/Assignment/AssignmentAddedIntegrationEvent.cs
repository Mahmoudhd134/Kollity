namespace Kollity.Services.Contracts.Assignment;

public class AssignmentAddedIntegrationEvent
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public AssignmentType Type { get; set; }
    public byte Degree { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime OpenUntilDate { get; set; }
    public Guid RoomId { get; set; }
    public Guid DoctorId { get; set; }
}