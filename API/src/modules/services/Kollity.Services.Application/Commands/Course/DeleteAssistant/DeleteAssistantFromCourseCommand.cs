using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Dtos.Course;

namespace Kollity.Services.Application.Commands.Course.DeleteAssistant;

public record DeleteAssistantFromCourseCommand(CourseDoctorIdsMap CourseDoctorIdsMap) : ICommand;