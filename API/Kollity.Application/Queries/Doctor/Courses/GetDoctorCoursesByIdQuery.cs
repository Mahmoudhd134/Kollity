using Kollity.Application.Dtos.Course;

namespace Kollity.Application.Queries.Doctor.Courses;

public record GetDoctorCoursesByIdQuery(Guid Id) : IQuery<List<CourseForListDto>>;