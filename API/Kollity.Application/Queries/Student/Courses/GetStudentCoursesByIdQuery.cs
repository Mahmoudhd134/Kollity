using Kollity.Application.Dtos.Course;

namespace Kollity.Application.Queries.Student.Courses;

public record GetStudentCoursesByIdQuery(Guid Id) : IQuery<List<CourseForListDto>>;