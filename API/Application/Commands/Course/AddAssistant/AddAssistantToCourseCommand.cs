using Application.Dtos.Course;

namespace Application.Commands.Course.AddAssistant;

public record AddAssistantToCourseCommand(CourseDoctorIdsMap CourseDoctorIdsMap) : ICommand;