using Application.Dtos.Course;

namespace Application.Commands.Course.DeAssignStudent;

public record DeAssignStudentFromCourseCommand(CourseStudentIdsMap Ids) : ICommand;