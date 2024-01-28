namespace Application.Dtos.Student;

public class CourseForStudentDto
{
    public Guid Id { get; set; }
    public string Department { get; set; }
    public int Code { get; set; }
    public string Name { get; set; }
}