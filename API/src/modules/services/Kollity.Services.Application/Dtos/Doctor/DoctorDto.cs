using Kollity.Services.Application.Dtos.Course;

namespace Kollity.Services.Application.Dtos.Doctor;

public class DoctorDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string ProfileImage { get; set; }
    public string FullNameInArabic { get; set; }
    public List<CourseForListDto> Courses { get; set; }
}