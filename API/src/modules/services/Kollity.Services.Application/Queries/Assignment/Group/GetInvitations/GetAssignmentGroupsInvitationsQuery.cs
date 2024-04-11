using Kollity.Services.Application.Dtos.Assignment.Group;

namespace Kollity.Services.Application.Queries.Assignment.Group.GetInvitations;

public record GetAssignmentGroupsInvitationsQuery(Guid RoomId) : IQuery<List<AssignmentGroupInvitationDto>>;