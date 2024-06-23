namespace Kollity.Exams.Application.Dtos.Exam;

public class ExamDegreesDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int CountOfUsersSolve { get; set; }
    public int ExamTotalDegree { get; set; }
    public List<UserForExamDegreesDto> Users { get; set; }
}