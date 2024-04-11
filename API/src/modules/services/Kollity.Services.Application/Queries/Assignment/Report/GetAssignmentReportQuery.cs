using Kollity.Services.Application.Dtos;
using Kollity.Services.Application.Dtos.Assignment;

namespace Kollity.Services.Application.Queries.Assignment.Report;

public record GetAssignmentReportQuery(Guid Id, PaginationDto Pagination) : IQuery<AssignmentReportDto>;