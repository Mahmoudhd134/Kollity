using Kollity.Services.Application.Abstractions.Messages;

namespace Kollity.Services.Application.Commands.Course.DeAssignDoctor;

public record DeAssignDoctorFromCourseCommand(Guid CourseId) : ICommand;