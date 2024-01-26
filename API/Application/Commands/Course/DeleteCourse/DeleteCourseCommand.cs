namespace Application.Commands.Course.DeleteCourse;

public record DeleteCourseCommand(Guid Id) : ICommand;