namespace Kollity.Services.Application.Dtos.Student;

public class StudentProfileDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string FullNameInArabic { get; set; }
    public string Code { get; set; }
    public string ProfileImage { get; set; }
}