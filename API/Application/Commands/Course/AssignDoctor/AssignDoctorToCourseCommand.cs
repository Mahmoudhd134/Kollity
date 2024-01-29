using Application.Dtos.Course;

namespace Application.Commands.Course.AssignDoctor;

public record AssignDoctorToCourseCommand(CourseDoctorIdsMap CourseDoctorIdsMap) : ICommand;