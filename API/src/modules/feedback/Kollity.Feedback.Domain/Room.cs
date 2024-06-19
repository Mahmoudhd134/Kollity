namespace Kollity.Feedback.Domain;

public class Room
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid CourseId { get; set; }
    public Course Course { get; set; }
    public Guid DoctorId { get; set; }
    public User Doctor { get; set; }
    public bool IsDeleted { get; set; }
    public List<Exam> Exams { get; set; } = [];
    public List<RoomUser> RoomUsers { get; set; } = [];
}