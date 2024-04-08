namespace Kollity.Services.Application.Dtos.Exam;

public class EditExamQuestionDto
{
    public Guid Id { get; set; }
    public string Question { get; set; }
    public int OpenForSeconds { get; set; }
    public int Degree { get; set; }
}