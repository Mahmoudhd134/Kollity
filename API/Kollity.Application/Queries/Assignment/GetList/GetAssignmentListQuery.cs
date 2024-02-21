using Kollity.Application.Dtos.Assignment;

namespace Kollity.Application.Queries.Assignment.GetList;

public record GetAssignmentListQuery(Guid RoomId) : IQuery<List<AssignmentForListDto>>;