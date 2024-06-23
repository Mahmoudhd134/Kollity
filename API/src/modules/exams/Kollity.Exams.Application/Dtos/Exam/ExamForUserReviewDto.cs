namespace Kollity.Exams.Application.Dtos.Exam;

public class ExamForUserReviewDto
{
    public Guid Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Name { get; set; }
    public int Degree { get; set; }
    public int UserDegree { get; set; }
    public List<ExamQuestionForUserReviewDto> Questions { get; set; }
}