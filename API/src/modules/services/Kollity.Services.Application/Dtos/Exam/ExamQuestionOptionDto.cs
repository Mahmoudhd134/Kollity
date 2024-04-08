namespace Kollity.Services.Application.Dtos.Exam;

public class ExamQuestionOptionDto
{
    public Guid Id { get; set; }
    public string Option { get; set; }
    public bool IsRightOption { get; set; }
}