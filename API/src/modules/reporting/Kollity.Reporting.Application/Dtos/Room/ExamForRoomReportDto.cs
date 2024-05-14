namespace Kollity.Reporting.Application.Dtos.Room;

public class ExamForRoomReportDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime StartsAt { get; set; }
    public DateTime EndsAt { get; set; }
    public int NumberOfQuestions { get; set; }
    public int TotalDegree { get; set; }
    public int NumberOfAnswers { get; set; }
    public int? MaxStudentDegree { get; set; }
    public int? MinStudentDegree { get; set; }
    public double? AvgStudentDegree { get; set; }
}