﻿using Domain.CourseModels;
using Domain.Identity.User;

namespace Domain.RoomModels;

public class RoomsSupervisors
{
    public Guid Id { get; set; }
    public Guid RoomId { get; set; }
    public Room Room { get; set; }
    public Guid CourseId { get; set; }
    public Course Course { get; set; }
    public Guid SupervisorId { get; set; }
    public BaseUser Supervisor { get; set; }
}