﻿using Domain.CourseModels;
using Domain.RoomModels;

namespace Domain.AssignmentModels.AssignmentGroupModels;

public class AssignmentGroup
{
    public Guid Id { get; set; }
    public int Code { get; set; }
    public Guid RoomId { get; set; }
    public Room Room { get; set; }
    public Guid CourseId { get; set; }
    public Course Course { get; set; }
    public List<AssignmentAnswer> AssignmentsAnswers { get; set; } = [];
    public List<AssignmentGroupStudent> AssignmentGroupsStudents { get; set; } = [];
}