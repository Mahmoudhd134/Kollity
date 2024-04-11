using Kollity.Services.Application.Dtos.Course;

namespace Kollity.Services.Application.Commands.Course.AssignStudent;

public record AssignStudentToCourseCommand(CourseStudentIdsMap CourseStudentIdsMap) : ICommand;