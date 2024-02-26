namespace Kollity.Application.Dtos.Course;

public class RoomForCourseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid DoctorId { get; set; }
    public string DoctorName { get; set; }
}