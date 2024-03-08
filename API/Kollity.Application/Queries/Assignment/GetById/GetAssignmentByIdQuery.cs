using Kollity.Application.Dtos.Assignment;

namespace Kollity.Application.Queries.Assignment.GetById;

public record GetAssignmentByIdQuery(Guid AssignmentId) : IQuery<AssignmentDto>;