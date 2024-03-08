namespace Kollity.Application.Dtos.Course;

public class CourseDto
{
    public Guid Id { get; set; }
    public string Department { get; set; }
    public int Code { get; set; }
    public int Hours { get; set; }
    public string Name { get; set; }

    public bool HasADoctor { get; set; }
    public bool IsTheDoctorAssigned { get; set; }
    public DoctorForCourseDto Doctor { get; set; }
    public List<AssistantForCourseDto> Assistants { get; set; }
    public List<RoomForCourseDto> Rooms { get; set; }
}