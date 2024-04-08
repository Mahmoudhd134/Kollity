using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Dtos.Course;

namespace Kollity.Services.Application.Queries.Course.GetById;

public record GetCourseByIdQuery(Guid Id) : IQuery<CourseDto>;