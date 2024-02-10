using Kollity.Application.Dtos.Course;

namespace Kollity.Application.Commands.Course.Edit;

public record EditCourseCommand(EditCourseDto EditCourseDto) : ICommand;