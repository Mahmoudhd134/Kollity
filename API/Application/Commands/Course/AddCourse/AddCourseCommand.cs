using Application.Dtos.Course;

namespace Application.Commands.Course.AddCourse;

public record AddCourseCommand(AddCourseDto AddCourseDto) : ICommand;