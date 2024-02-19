using Kollity.Application.Commands.Assignment.Group.AcceptInvitation;
using Kollity.Application.Commands.Assignment.Group.AddGroup;
using Kollity.Application.Commands.Assignment.Group.CancelInvitation;
using Kollity.Application.Commands.Assignment.Group.DeclineInvitation;
using Kollity.Application.Commands.Assignment.Group.DeleteStudent;
using Kollity.Application.Commands.Assignment.Group.Leave;
using Kollity.Application.Commands.Assignment.Group.SendInvitation;
using Kollity.Application.Dtos.Assignment.Group;
using Kollity.Application.Queries.Assignment.Group.GetAll;
using Kollity.Application.Queries.Assignment.Group.GetById;
using Kollity.Application.Queries.Assignment.Group.GetInvitations;
using Kollity.Application.Queries.Assignment.Group.GetUserGroup;
using Kollity.Domain.Identity.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Kollity.API.Controllers;

[Route("api/room/{roomId:guid}/assignment-group")]
public class AssignmentGroupController : BaseController
{
    [HttpGet, Authorize(Roles = $"{Role.Doctor},{Role.Assistant},{Role.Admin}"),
     SwaggerResponse(200, type: typeof(List<AssignmentGroupForListDto>))]
    public Task<IResult> GetAll(Guid roomId)
    {
        return Send(new GetAllAssignmentGroupsForRoomQuery(roomId));
    }

    [HttpGet("my-group"), SwaggerResponse(200, type: typeof(AssignmentGroupDto))]
    public Task<IResult> Get(Guid roomId)
    {
        return Send(new GetUserAssignmentGroupQuery(roomId));
    }

    [HttpGet("{groupId:guid}"), Authorize(Roles = $"{Role.Doctor},{Role.Assistant},{Role.Admin}"),
     SwaggerResponse(200, type: typeof(AssignmentGroupDto))]
    public Task<IResult> Get(Guid roomId, Guid groupId)
    {
        return Send(new GetAssignmentGroupByIdQuery(groupId));
    }

    [HttpGet("invitations"), SwaggerResponse(200, type: typeof(List<AssignmentGroupInvitationDto>))]
    public Task<IResult> GetInvitations(Guid roomId)
    {
        return Send(new GetAssignmentGroupsInvitationsQuery(roomId));
    }

    [HttpPost, SwaggerResponse(200, type: typeof(AssignmentGroupDto))]
    public Task<IResult> Add(Guid roomId, [FromBody] AddAssignmentGroupDto dto)
    {
        return Send(new AddAssignmentGroupCommand(roomId, dto));
    }

    [HttpPatch("{groupId:guid}/accept-invitation")]
    public Task<IResult> AcceptInvitation(Guid roomId, Guid groupId)
    {
        return Send(new AcceptAssignmentGroupInvitationCommand(groupId));
    }

    [HttpPost("{groupId:guid}/send-invitation/{studentId:guid}")]
    public Task<IResult> SendInvitation(Guid roomId, Guid groupId, Guid studentId)
    {
        return Send(new SendAssignmentGroupJoinInvitationCommand(new AssignmentGroupInvitationMapDto
        {
            GroupId = groupId,
            StudentId = studentId
        }));
    }

    [HttpDelete("{groupId:guid}/cancel-invitation/{studentId:guid}")]
    public Task<IResult> CancelInvitation(Guid roomId, Guid groupId, Guid studentId)
    {
        return Send(new CancelJoinAssignmentGroupInvitationCommand(new AssignmentGroupInvitationMapDto
        {
            GroupId = groupId,
            StudentId = studentId
        }));
    }

    [HttpDelete("{groupId:guid}/decline-invitation")]
    public Task<IResult> DeclineInvitation(Guid roomId, Guid groupId)
    {
        return Send(new DeclineAssignmentGroupJoinInvitationCommand(groupId));
    }

    [HttpDelete("{groupId:guid}/delete-student/{studentId:guid}")]
    public Task<IResult> DeleteInvitation(Guid roomId, Guid groupId, Guid studentId)
    {
        return Send(new DeleteStudentFromAssignmentGroupCommand(new AssignmentGroupInvitationMapDto
        {
            GroupId = groupId,
            StudentId = studentId
        }));
    }


    [HttpDelete("{groupId:guid}/leave")]
    public Task<IResult> Leave(Guid roomId, Guid groupId)
    {
        return Send(new LeaveAssignmentGroupCommand(groupId));
    }
}