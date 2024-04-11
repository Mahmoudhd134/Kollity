using Kollity.Services.Application.Dtos.Course;

namespace Kollity.Services.Application.Commands.Course.Edit;

public record EditCourseCommand(EditCourseDto EditCourseDto) : ICommand;