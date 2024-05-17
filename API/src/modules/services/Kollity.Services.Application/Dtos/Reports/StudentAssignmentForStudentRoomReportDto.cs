using Kollity.Services.Domain.AssignmentModels;

namespace Kollity.Services.Application.Dtos.Reports;

public class StudentAssignmentForStudentRoomReportDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public AssignmentMode Mode { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime? SubmitDate { get; set; }
    public int? StudentDegree { get; set; }
}