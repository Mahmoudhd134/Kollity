namespace Kollity.Services.Application.Dtos.Reports;

public class StudentExamForStudentRoomReportDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int TotalDegree { get; set; }
    public int NumberOfQuestions { get; set; }
    public int? StudentDegree { get; set; }
}