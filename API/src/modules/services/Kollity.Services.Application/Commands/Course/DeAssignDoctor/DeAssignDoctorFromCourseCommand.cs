﻿namespace Kollity.Services.Application.Commands.Course.DeAssignDoctor;

public record DeAssignDoctorFromCourseCommand(Guid CourseId) : ICommand;