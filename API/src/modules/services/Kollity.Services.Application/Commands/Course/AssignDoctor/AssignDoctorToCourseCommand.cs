using Kollity.Services.Application.Dtos.Course;

namespace Kollity.Services.Application.Commands.Course.AssignDoctor;

public record AssignDoctorToCourseCommand(CourseDoctorIdsMap CourseDoctorIdsMap) : ICommand;