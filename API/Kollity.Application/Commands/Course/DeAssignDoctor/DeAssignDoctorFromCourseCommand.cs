namespace Kollity.Application.Commands.Course.DeAssignDoctor;

public record DeAssignDoctorFromCourseCommand(Guid CourseId) : ICommand;