using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Dtos.Assignment;

namespace Kollity.Services.Application.Queries.Assignment.GetById;

public record GetAssignmentByIdQuery(Guid AssignmentId) : IQuery<AssignmentDto>;