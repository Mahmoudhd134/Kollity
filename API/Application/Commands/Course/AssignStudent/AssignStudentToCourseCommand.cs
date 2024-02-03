using Application.Dtos.Course;

namespace Application.Commands.Course.AssignStudent;

public record AssignStudentToCourseCommand(CourseStudentIdsMap CourseStudentIdsMap) : ICommand;