using Kollity.Application.Dtos.Course;

namespace Kollity.Application.Queries.Course.GetById;

public record GetCourseByIdQuery(Guid Id) : IQuery<CourseDto>;