namespace Kollity.Application.Dtos.Course;

public class RoomForCourseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; } = "default.png";
}