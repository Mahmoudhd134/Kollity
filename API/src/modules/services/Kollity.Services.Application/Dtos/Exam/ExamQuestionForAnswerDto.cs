namespace Kollity.Services.Application.Dtos.Exam;

public class ExamQuestionForAnswerDto
{
    public Guid Id { get; set; }
    public string Question { get; set; }
    public int OpenForSeconds { get; set; }
    public byte Degree { get; set; }
    public int QuestionNumber { get; set; }
    public int QuestionsCount { get; set; }
    public List<ExamQuestionOptionForAnswerDto> Options { get; set; }
}