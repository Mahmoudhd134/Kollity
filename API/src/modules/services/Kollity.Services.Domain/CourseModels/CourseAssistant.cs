﻿using Kollity.Services.Domain.DoctorModels;

namespace Kollity.Services.Domain.CourseModels;

public class CourseAssistant
{
    public Guid Id { get; set; }

    public Guid CourseId { get; set; }
    public Course Course { get; set; }

    public Guid AssistantId { get; set; }
    public Doctor Assistant { get; set; }
}