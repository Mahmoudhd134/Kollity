using Kollity.Domain.AssignmentModels;
using Kollity.Domain.AssignmentModels.AssignmentGroupModels;
using Kollity.Domain.CourseModels;
using Kollity.Domain.DoctorModels;
using Kollity.Domain.ExamModels;

namespace Kollity.Domain.RoomModels;

public class Room
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; } = "default.png";
    public Guid CourseId { get; set; }
    public Course Course { get; set; }
    public Guid? DoctorId { get; set; }
    public Doctor Doctor { get; set; }
    public bool EnsureJoinRequest { get; set; }

    public List<RoomMessage> RoomMessages { get; set; } = [];

    public List<UserRoom> UsersRooms { get; set; } = [];

    // public List<RoomSupervisor> RoomsSupervisors { get; set; } = [];
    public List<Exam> Exams { get; set; } = [];
    public List<Assignment> Assignments { get; set; } = [];
    public List<AssignmentGroup> AssignmentGroups { get; set; } = [];
}