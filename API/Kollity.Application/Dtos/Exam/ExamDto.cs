namespace Kollity.Application.Dtos.Exam;

public class ExamDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<ExamQuestionDto> Questions { get; set; }
    public int NumberOfQuestions { get; set; }
    public int Degree { get; set; }
    public int TotalTime { get; set; }
}