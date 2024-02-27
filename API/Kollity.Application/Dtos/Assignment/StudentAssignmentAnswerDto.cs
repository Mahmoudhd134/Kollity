namespace Kollity.Application.Dtos.Assignment;

public class StudentAssignmentAnswerDto
{
    public Guid Id { get; set; }
    public string ProfileImage { get; set; }
    public string FullName { get; set; }
    public string Code { get; set; }
    public DateTime UploadDate { get; set; }
    public byte? Degree { get; set; }
    public Guid AnswerId { get; set; }
}