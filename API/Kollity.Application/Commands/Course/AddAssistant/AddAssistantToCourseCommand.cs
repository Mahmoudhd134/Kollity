using Kollity.Application.Dtos.Course;

namespace Kollity.Application.Commands.Course.AddAssistant;

public record AddAssistantToCourseCommand(CourseDoctorIdsMap CourseDoctorIdsMap) : ICommand;