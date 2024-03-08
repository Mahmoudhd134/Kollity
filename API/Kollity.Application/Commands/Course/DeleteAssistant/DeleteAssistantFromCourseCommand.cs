using Kollity.Application.Dtos.Course;

namespace Kollity.Application.Commands.Course.DeleteAssistant;

public record DeleteAssistantFromCourseCommand(CourseDoctorIdsMap CourseDoctorIdsMap) : ICommand;