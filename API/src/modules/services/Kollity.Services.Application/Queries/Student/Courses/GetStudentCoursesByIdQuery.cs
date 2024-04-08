using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Dtos.Course;

namespace Kollity.Services.Application.Queries.Student.Courses;

public record GetStudentCoursesByIdQuery(Guid Id) : IQuery<List<CourseForListDto>>;