using Kollity.Reporting.Application.Dtos.Course;

namespace Kollity.Reporting.Application.Queries.Course;

public record GetCourseReportQuery(Guid Id, DateTime? From, DateTime? To) : IQuery<CourseReportDto>;