using System.Globalization;
using Kollity.API.Extensions;
using Kollity.Application.Commands.Room.AcceptAllJoins;
using Kollity.Application.Commands.Room.AcceptJoin;
using Kollity.Application.Commands.Room.Add;
using Kollity.Application.Commands.Room.AddContent;
using Kollity.Application.Commands.Room.AddSupervisor;
using Kollity.Application.Commands.Room.Delete;
using Kollity.Application.Commands.Room.DeleteContent;
using Kollity.Application.Commands.Room.DeleteSupervisor;
using Kollity.Application.Commands.Room.DenyJoin;
using Kollity.Application.Commands.Room.Edit;
using Kollity.Application.Commands.Room.Join;
using Kollity.Application.Dtos.Room;
using Kollity.Application.Queries.Room.GetById;
using Kollity.Application.Queries.Room.GetContent;
using Kollity.Application.Queries.Room.GetMembers;
using Kollity.Application.Queries.Room.GetSingleContent;
using Kollity.Domain.Identity.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Kollity.API.Controllers;

public class RoomController : BaseController
{
    // start room specific endpoints

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

    [HttpPost("accept-join-request")]
    public Task<IResult> AcceptJoinRequest(RoomUserIdsMap ids)
    {
        return Send(new AcceptRoomJoinRequestCommand(ids));
    }

    [HttpPost("{id:guid}/accept-all-join-requests")]
    public Task<IResult> AcceptAllJoinRequests(Guid id)
    {
        return Send(new AcceptAllRoomJoinRequestsCommand(id));
    }

    [HttpPost("add-supervisor")]
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

    [HttpDelete("deny-join-request")]
    public Task<IResult> DenyJoinRequest(RoomUserIdsMap ids)
    {
        return Send(new DenyRoomJoinRequestCommand(ids));
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = $"{Role.Admin},{Role.Doctor},{Role.Assistant}")]
    public Task<IResult> Delete(Guid id)
    {
        return Send(new DeleteRoomCommand(id));
    }

    [HttpDelete("delete-supervisor")]
    public Task<IResult> DeleteSupervisor([FromBody] RoomUserIdsMap ids)
    {
        return Send(new DeleteRoomSupervisorCommand(ids));
    }

    //end room specific endpoints


    //start room content specific endpoints

    [HttpPost("{id:guid}/content")]
    [RequestSizeLimit(MaxFileSize)]
    [RequestFormLimits(MultipartBodyLengthLimit = MaxFileSize)]
    public Task<IResult> AddContent(Guid id, [FromForm] AddRoomContentDto addRoomContentDto)
    {
        return Send(new AddRoomContentCommand(id, addRoomContentDto));
    }

    [HttpGet("{id:guid}/content"), SwaggerResponse(200, type: typeof(List<RoomContentDto>))]
    public Task<IResult> GetContent(Guid id)
    {
        return Send(new GetRoomContentQuery(id));
    }

    [AllowAnonymous, HttpGet("content/{contentId:guid}"), SwaggerResponse(200, type: typeof(File))]
    public async Task<ActionResult> GetSingleContent(Guid contentId)
    {
        var response = await Sender.Send(new GetRoomSingleContentQuery(contentId));
        if (response.IsSuccess == false)
            return response.ToActionResult();

        Response.Headers.Append("Content-Disposition",
            "attachment; filename=" + response.Data.Name + response.Data.Extension);
        Response.Headers.Append("Content-Type", "application/octet-stream");
        Response.Headers.Append("Content-Length", response.Data.Size.ToString(CultureInfo.InvariantCulture));
        await response.Data.Stream.CopyToAsync(Response.Body);
        response.Data.Stream.Close();
        return new EmptyResult();
    }

    [HttpDelete("{roomId:guid}/content/{contentId:guid}")]
    public Task<IResult> DeleteContent(Guid roomId, Guid contentId)
    {
        return Send(new DeleteRoomContentCommand(roomId, contentId));
    }

    //end room content specific endpoints
}