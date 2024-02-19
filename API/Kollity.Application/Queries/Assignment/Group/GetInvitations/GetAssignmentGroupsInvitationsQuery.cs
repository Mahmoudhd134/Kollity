using Kollity.Application.Dtos.Assignment;
using Kollity.Application.Dtos.Assignment.Group;

namespace Kollity.Application.Queries.Assignment.Group.GetInvitations;

public record GetAssignmentGroupsInvitationsQuery(Guid RoomId) : IQuery<List<AssignmentGroupInvitationDto>>;