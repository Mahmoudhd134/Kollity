﻿namespace Kollity.Services.Contracts.Course;

public class AssistantAssignedToCourseIntegrationEvent
{
    public Guid CourseId { get; set; }
    public Guid AssistantId { get; set; }
}