﻿using Domain.CourseModels;
using Domain.RoomModels;
using Domain.StudentModels;

namespace Domain.AssignmentModels.AssignmentGroupModels;

public class AssignmentGroupsStudents
{
    public Guid Id { get; set; }
    public Guid AssignmentGroupId { get; set; }
    public AssignmentGroup AssignmentGroup { get; set; }
    public Guid RoomId { get; set; }
    public Room Room { get; set; }
    public Guid CourseId { get; set; }
    public Course Course { get; set; }
    public Guid StudentId { get; set; }
    public Student Student { get; set; }
    public bool JoinRequestAccepted { get; set; }
}