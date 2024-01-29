namespace Application.Commands.Course.Delete;

public record DeleteCourseCommand(Guid Id) : ICommand;