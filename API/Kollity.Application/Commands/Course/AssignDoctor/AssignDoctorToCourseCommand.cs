using Kollity.Application.Dtos.Course;

namespace Kollity.Application.Commands.Course.AssignDoctor;

public record AssignDoctorToCourseCommand(CourseDoctorIdsMap CourseDoctorIdsMap) : ICommand;