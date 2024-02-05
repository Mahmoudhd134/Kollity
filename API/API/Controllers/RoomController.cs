using Application.Commands.Room.AcceptAllJoins;
using Application.Commands.Room.AcceptJoin;
using Application.Commands.Room.Add;
using Application.Commands.Room.Delete;
using Application.Commands.Room.DenyJoin;
using Application.Commands.Room.Edit;
using Application.Commands.Room.Join;
using Application.Dtos.Room;
using Application.Queries.Room.GetById;
using Application.Queries.Room.GetMembers;
using Domain.Identity.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

public class RoomController : BaseController
{
    [HttpPost, Authorize(Roles = $"{Role.Doctor},{Role.Assistant}")]
    public Task<IResult> Add(AddRoomDto addRoomDto) => Send(new AddRoomCommand(addRoomDto));
    
    [HttpPost("{id:guid}/join")]
    public Task<IResult> Join(Guid id) => Send(new JoinRoomCommand(id));

    [HttpPost("accept-join-request")]
    public Task<IResult> AcceptJoinRequest(RoomUserIdsMap ids) => Send(new AcceptRoomJoinRequestCommand(ids));

    [HttpPost("{id:guid}/accept-all-join-requests")]
    public Task<IResult> AcceptAllJoinRequests(Guid id) => Send(new AcceptAllRoomJoinRequestsCommand(id));
    
    [HttpGet("{id:guid}"), SwaggerResponse(200, type: typeof(RoomDto))]
    public Task<IResult> Get(Guid id) => Send(new GetRoomByIdQuery(id));

    [HttpGet("{id:guid}/members"), SwaggerResponse(200, type: typeof(List<RoomMemberDto>))]
    public Task<IResult> GetMembers(Guid id) => Send(new GetRoomMembersQuery(id));

    [HttpPut, Authorize(Roles = $"{Role.Doctor},{Role.Assistant}")]
    public Task<IResult> Edit(EditRoomDto editRoomDto) => Send(new EditRoomCommand(editRoomDto));

    [HttpDelete("deny-join-request")]
    public Task<IResult> DenyJoinRequest(RoomUserIdsMap ids) => Send(new DenyRoomJoinRequestCommand(ids));

    [HttpDelete("{id:guid}"), Authorize(Roles = $"{Role.Admin},{Role.Doctor},{Role.Assistant}")]
    public Task<IResult> Delete(Guid id) => Send(new DeleteRoomCommand(id));
}