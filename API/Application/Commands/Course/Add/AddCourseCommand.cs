using Application.Dtos.Course;

namespace Application.Commands.Course.Add;

public record AddCourseCommand(AddCourseDto AddCourseDto) : ICommand;