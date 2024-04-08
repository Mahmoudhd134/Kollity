namespace Kollity.Services.Application.Dtos.Assignment;

public class AnswerDto
{
    public Guid Id { get; set; }
    public DateTime SolveDate { get; set; }
    public byte? Degree { get; set; }
}