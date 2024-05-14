using Kollity.Reporting.Application.Dtos.User;

namespace Kollity.Reporting.Application.Dtos.Room;

public class RoomReportDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public int MembersCount { get; set; }
    public UserUsernameDto RoomDoctor { get; set; }

    public Guid CourseId { get; set; }
    public string CourseName { get; set; }

    public List<ExamForRoomReportDto> Exams { get; set; }
    public List<AssignmentForRoomReportDto> Assignments { get; set; }
    public List<RoomContentForRoomReportDto> Contents { get; set; }
}