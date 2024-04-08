using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Dtos.Course;

namespace Kollity.Services.Application.Commands.Course.DeAssignStudent;

public record DeAssignStudentFromCourseCommand(CourseStudentIdsMap Ids) : ICommand;