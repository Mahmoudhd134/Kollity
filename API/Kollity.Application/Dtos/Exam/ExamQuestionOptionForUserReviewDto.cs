namespace Kollity.Application.Dtos.Exam;

public class ExamQuestionOptionForUserReviewDto
{
    public Guid Id { get; set; }
    public string Option { get; set; }
    public bool IsRightOption { get; set; }
}