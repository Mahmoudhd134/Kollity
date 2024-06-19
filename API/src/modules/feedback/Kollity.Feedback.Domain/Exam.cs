namespace Kollity.Feedback.Domain;

public class Exam
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid RoomId { get; set; }
    public Room Room { get; set; }
    public bool IsDeleted { get; set; }
}