using Kollity.Reporting.Domain.AssignmentModels;
using Kollity.Reporting.Domain.CourseModels;
using Kollity.Reporting.Domain.ExamModels;
using Kollity.Reporting.Domain.UserModels;

namespace Kollity.Reporting.Domain.RoomModels;

public class Room
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid CourseId { get; set; }
    public Course Course { get; set; }
    public Guid DoctorId { get; set; }
    public Doctor Doctor { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsDeleted { get; set; }

    public List<RoomUser> RoomUsers { get; set; } = [];
    public List<Exam> Exams { get; set; } = [];
    public List<ExamAnswer> ExamAnswers { get; set; } = [];
    public List<Assignment> Assignments { get; set; } = [];
    public List<AssignmentAnswer> AssignmentAnswers { get; set; } = [];
    public List<AssignmentGroup> AssignmentGroups { get; set; } = [];
}