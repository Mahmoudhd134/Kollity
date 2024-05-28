using Kollity.Reporting.Application.Dtos.Assignment;

namespace Kollity.Reporting.Application.Queries.Assignment;

public record GetAssignmentStatisticsQuery(Guid Id) : IQuery<AssignmentStatisticsDto>;