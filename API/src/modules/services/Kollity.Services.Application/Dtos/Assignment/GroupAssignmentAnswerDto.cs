namespace Kollity.Services.Application.Dtos.Assignment;

public class GroupAssignmentAnswerDto
{
    public Guid Id { get; set; }
    public int Code { get; set; }
    public DateTime UploadDate { get; set; }
    public Guid AnswerId { get; set; }
}