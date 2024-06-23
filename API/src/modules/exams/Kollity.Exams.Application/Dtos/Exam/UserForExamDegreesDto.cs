namespace Kollity.Exams.Application.Dtos.Exam;

public class UserForExamDegreesDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Code { get; set; }
    public int? Degree { get; set; }
}