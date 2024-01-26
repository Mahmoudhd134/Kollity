using Application.Dtos.Course;

namespace Application.Commands.Course.EditCourse;

public record EditCourseCommand(Guid CourseId, EditCourseDto EditCourseDto) : ICommand;