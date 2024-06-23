using Kollity.Services.Domain.AssignmentModels;
using Kollity.Services.Domain.AssignmentModels.AssignmentGroupModels;
using Kollity.Services.Domain.CourseModels;
using Kollity.Services.Domain.DoctorModels;

namespace Kollity.Services.Domain.RoomModels;

public class Room
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid CourseId { get; set; }
    public Course Course { get; set; }
    public Guid? DoctorId { get; set; }
    public Doctor Doctor { get; set; }
    public bool EnsureJoinRequest { get; set; }
    public byte AssignmentGroupMaxLength { get; set; }
    public bool AssignmentGroupOperationsEnabled { get; set; }

    public List<RoomMessage> RoomMessages { get; set; } = [];

    public List<UserRoom> UsersRooms { get; set; } = [];

    public List<RoomContent> RoomContents { get; set; } = [];
    public List<Assignment> Assignments { get; set; } = [];
    public List<AssignmentGroup> AssignmentGroups { get; set; } = [];
}