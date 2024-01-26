using Application.Dtos.Course;

namespace Application.Queries.Course.GetById;

public record GetCourseByIdQuery(Guid Id) : IQuery<CourseDto>;