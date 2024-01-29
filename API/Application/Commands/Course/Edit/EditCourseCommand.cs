using Application.Dtos.Course;

namespace Application.Commands.Course.Edit;

public record EditCourseCommand(EditCourseDto EditCourseDto) : ICommand;