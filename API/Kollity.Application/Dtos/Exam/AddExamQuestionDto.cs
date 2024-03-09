namespace Kollity.Application.Dtos.Exam;

public class AddExamQuestionDto
{
    public string Question { get; set; }
    public int OpenForSeconds { get; set; }
    public int Degree { get; set; }
}