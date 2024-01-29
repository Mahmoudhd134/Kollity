using Application.Dtos.Course;

namespace Application.Commands.Course.DeleteAssistant;

public record DeleteAssistantFromCourseCommand(CourseDoctorIdsMap CourseDoctorIdsMap) : ICommand;