﻿namespace Kollity.Services.Contracts.Course;

public class DoctorAssignedToCourseIntegrationEvent
{
    public Guid CourseId { get; set; }
    public Guid DoctorId { get; set; }
}