using Domain.AssignmentModels.AssignmentGroupModels;
using Domain.CourseModels;

namespace Domain.RoomModels;

public class Room
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; } = "default.png";
    public Guid CourseId { get; set; }
    public Course Course { get; set; }
    public bool EnsureJoinRequest { get; set; }

    public List<RoomMessage> RoomMessages { get; set; } = [];
    public List<UsersRooms> UsersRooms { get; set; } = [];
    public List<RoomsSupervisors> RoomsSupervisors { get; set; } = [];
    public List<AssignmentGroupsStudents> AssignmentGroupsStudents { get; set; } = [];
}