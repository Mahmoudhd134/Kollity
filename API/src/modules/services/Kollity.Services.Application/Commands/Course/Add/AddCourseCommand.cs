using Kollity.Services.Application.Dtos.Course;

namespace Kollity.Services.Application.Commands.Course.Add;

public record AddCourseCommand(AddCourseDto AddCourseDto) : ICommand;