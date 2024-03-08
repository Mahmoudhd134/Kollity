using Kollity.Application.Dtos.Course;

namespace Kollity.Application.Commands.Course.DeAssignStudent;

public record DeAssignStudentFromCourseCommand(CourseStudentIdsMap Ids) : ICommand;