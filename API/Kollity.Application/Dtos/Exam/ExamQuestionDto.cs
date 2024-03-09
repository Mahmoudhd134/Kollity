namespace Kollity.Application.Dtos.Exam;

public class ExamQuestionDto
{
    public Guid Id { get; set; }
    public string Question { get; set; }
    public int OpenForSeconds { get; set; }
    public byte Degree { get; set; }
    public List<ExamQuestionOptionDto> Options { get; set; }
}