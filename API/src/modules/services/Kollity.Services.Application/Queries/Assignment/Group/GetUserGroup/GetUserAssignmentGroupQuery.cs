using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Dtos.Assignment.Group;

namespace Kollity.Services.Application.Queries.Assignment.Group.GetUserGroup;

public record GetUserAssignmentGroupQuery(Guid RoomId) : IQuery<AssignmentGroupDto>;