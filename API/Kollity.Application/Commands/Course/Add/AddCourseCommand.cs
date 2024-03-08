using Kollity.Application.Dtos.Course;

namespace Kollity.Application.Commands.Course.Add;

public record AddCourseCommand(AddCourseDto AddCourseDto) : ICommand;