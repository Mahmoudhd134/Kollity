using Kollity.Reporting.Domain.AssignmentModels;

namespace Kollity.Reporting.Application.Dtos.Room;

public class AssignmentForRoomReportDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ReportingAssignmentMode Mode { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime OpenTo { get; set; }
    public int Degree { get; set; }
    public int NumberOfAnswers { get; set; }
    public int? MaxStudentDegree { get; set; }
    public int? MinStudentDegree { get; set; }
    public double? AvgStudentDegree { get; set; }
}