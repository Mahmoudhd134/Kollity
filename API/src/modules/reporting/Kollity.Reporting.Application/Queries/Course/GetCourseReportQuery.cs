using Kollity.Reporting.Application.Dtos.Course;

namespace Kollity.Reporting.Application.Queries.Course;

public record GetCourseReportQuery(Guid Id) : IQuery<CourseReportDto>;