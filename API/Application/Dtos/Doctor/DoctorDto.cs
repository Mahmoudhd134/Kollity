using Application.Dtos.Course;

namespace Application.Dtos.Doctor;

public class DoctorDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string ProfileImage { get; set; }
    public List<CourseForListDto> Courses { get; set; }
}