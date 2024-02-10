using Kollity.Application.Dtos.Course;

namespace Kollity.Application.Commands.Course.AssignStudent;

public record AssignStudentToCourseCommand(CourseStudentIdsMap CourseStudentIdsMap) : ICommand;