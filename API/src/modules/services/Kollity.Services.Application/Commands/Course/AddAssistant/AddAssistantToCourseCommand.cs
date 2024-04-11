using Kollity.Services.Application.Dtos.Course;

namespace Kollity.Services.Application.Commands.Course.AddAssistant;

public record AddAssistantToCourseCommand(CourseDoctorIdsMap CourseDoctorIdsMap) : ICommand;