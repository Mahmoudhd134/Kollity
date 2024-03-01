using Kollity.Application.Dtos;
using Kollity.Application.Dtos.Assignment;

namespace Kollity.Application.Queries.Assignment.Report;

public record GetAssignmentReportQuery(Guid Id, PaginationDto Pagination) : IQuery<AssignmentReportDto>;