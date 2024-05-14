using Kollity.Reporting.Application.Dtos.Student;

namespace Kollity.Reporting.Application.Queries.Student;

public record StudentReportQuery(Guid Id, DateTime? From, DateTime? To) : IQuery<StudentReportDto>;