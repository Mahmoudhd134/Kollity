using Kollity.Application.Commands.Room.AcceptAllJoins;
using Kollity.Application.Commands.Room.AcceptJoin;
using Kollity.Application.Commands.Room.Add;
using Kollity.Application.Commands.Room.AddSupervisor;
using Kollity.Application.Commands.Room.Delete;
using Kollity.Application.Commands.Room.DeleteSupervisor;
using Kollity.Application.Commands.Room.DenyJoin;
using Kollity.Application.Commands.Room.Edit;
using Kollity.Application.Commands.Room.Join;
using Kollity.Application.Dtos.Room;
using Kollity.Application.Queries.Room.GetById;
using Kollity.Application.Queries.Room.GetMembers;
using Kollity.Domain.Identity.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Kollity.API.Controllers;

public class RoomController : BaseController
{
    [HttpPost]
    [Authorize(Roles = $"{Role.Doctor},{Role.Assistant}")]
    public Task<IResult> Add(AddRoomDto addRoomDto)
    {
        return Send(new AddRoomCommand(addRoomDto));
    }

    [HttpPost("{id:guid}/join")]
    public Task<IResult> Join(Guid id)
    {
        return Send(new JoinRoomCommand(id));
    }

    [HttpPatch("accept-join-request")]
    public Task<IResult> AcceptJoinRequest(RoomUserIdsMap ids)
    {
        return Send(new AcceptRoomJoinRequestCommand(ids));
    }

    [HttpPut("{id:guid}/accept-all-join-requests")]
    public Task<IResult> AcceptAllJoinRequests(Guid id)
    {
        return Send(new AcceptAllRoomJoinRequestsCommand(id));
    }

    [HttpPatch("add-supervisor")]
    public Task<IResult> AddSupervisor([FromBody] RoomUserIdsMap ids)
    {
        return Send(new AddRoomSupervisorCommand(ids));
    }

    [HttpGet("{id:guid}")]
    [SwaggerResponse(200, type: typeof(RoomDto))]
    public Task<IResult> Get(Guid id)
    {
        return Send(new GetRoomByIdQuery(id));
    }

    [HttpGet("{id:guid}/members")]
    [SwaggerResponse(200, type: typeof(List<RoomMemberDto>))]
    public Task<IResult> GetMembers(Guid id)
    {
        return Send(new GetRoomMembersQuery(id));
    }

    [HttpPut]
    [Authorize(Roles = $"{Role.Doctor},{Role.Assistant}")]
    public Task<IResult> Edit(EditRoomDto editRoomDto)
    {
        return Send(new EditRoomCommand(editRoomDto));
    }

    [HttpDelete("{roomId:guid}/deny-join-request/{userId:guid}")]
    public Task<IResult> DenyJoinRequest(Guid roomId, Guid userId)
    {
        return Send(new DenyRoomJoinRequestCommand(new RoomUserIdsMap()
        {
            RoomId = roomId,
            UserId = userId
        }));
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = $"{Role.Admin},{Role.Doctor},{Role.Assistant}")]
    public Task<IResult> Delete(Guid id)
    {
        return Send(new DeleteRoomCommand(id));
    }

    [HttpDelete("{roomId:guid}/delete-supervisor/{supervisorId:guid}")]
    public Task<IResult> DeleteSupervisor(Guid roomId, Guid supervisorId)
    {
        return Send(new DeleteRoomSupervisorCommand(new RoomUserIdsMap()
        {
            RoomId = roomId,
            UserId = supervisorId
        }));
    }
}