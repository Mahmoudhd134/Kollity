namespace Kollity.Contracts.Dto;

public class AssignmentCreatedEventDto
{
    public Guid AssignmentId { get; set; }
    public string AssignmentName { get; set; }
    public DateTime OpenUntil { get; set; }
    public Guid RoomId { get; set; }
    public string RoomName { get; set; }
    public string CourseName { get; set; }
    public List<UserEmailDto> Users { get; set; }
}