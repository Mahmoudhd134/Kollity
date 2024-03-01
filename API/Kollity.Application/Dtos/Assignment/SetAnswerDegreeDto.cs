namespace Kollity.Application.Dtos.Assignment;

public class SetAnswerDegreeDto
{
    public byte StudentDegree { get; set; }
    public Guid AnswerId { get; set; }
    public Guid StudentId { get; set; }
}