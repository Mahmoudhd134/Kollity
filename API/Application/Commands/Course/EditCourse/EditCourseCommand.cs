using Application.Dtos.Course;

namespace Application.Commands.Course.EditCourse;

public record EditCourseCommand(EditCourseDto EditCourseDto) : ICommand;