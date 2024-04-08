using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Dtos.Assignment;

namespace Kollity.Services.Application.Queries.Assignment.GetList;

public record GetAssignmentListQuery(Guid RoomId) : IQuery<List<AssignmentForListDto>>;