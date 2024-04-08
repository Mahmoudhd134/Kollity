using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Dtos.Course;

namespace Kollity.Services.Application.Queries.Doctor.Courses;

public record GetDoctorCoursesByIdQuery(Guid Id) : IQuery<List<CourseForListDto>>;