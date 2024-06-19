namespace Kollity.Feedback.Domain;

public class User
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string FullName { get; set; }
    public UserType UserType { get; set; }
    public bool IsDeleted { get; set; }
    public List<RoomUser> RoomUsers { get; set; } = [];
    public List<CourseStudent> Courses { get; set; } = [];
}