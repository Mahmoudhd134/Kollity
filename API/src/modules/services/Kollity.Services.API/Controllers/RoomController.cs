using Kollity.Services.API.Extensions;
using Kollity.Services.API.Hubs.Abstraction;
using Kollity.Services.API.Hubs.Hubs.Room;
using Kollity.Services.Application.Commands.Room.AcceptAllJoins;
using Kollity.Services.Application.Commands.Room.AcceptJoin;
using Kollity.Services.Application.Commands.Room.Add;
using Kollity.Services.Application.Commands.Room.AddSupervisor;
using Kollity.Services.Application.Commands.Room.Delete;
using Kollity.Services.Application.Commands.Room.DeleteSupervisor;
using Kollity.Services.Application.Commands.Room.DenyJoin;
using Kollity.Services.Application.Commands.Room.Edit;
using Kollity.Services.Application.Commands.Room.Join;
using Kollity.Services.Application.Commands.Room.Messages.Add;
using Kollity.Services.Application.Commands.Room.Messages.DeletePollSubmission;
using Kollity.Services.Application.Commands.Room.Messages.GetUnRead;
using Kollity.Services.Application.Commands.Room.Messages.SubmitPoll;
using Kollity.Services.Application.Dtos.Reports;
using Kollity.Services.Application.Dtos.Room;
using Kollity.Services.Application.Dtos.Room.Message;
using Kollity.Services.Application.Queries.Reports.UserRoomReport;
using Kollity.Services.Application.Queries.Room.GetById;
using Kollity.Services.Application.Queries.Room.GetMembers;
using Kollity.Services.Application.Queries.Room.Messages.GetListBeforeDate;
using Kollity.Services.Application.Queries.Room.Messages.GetPoll;
using Kollity.Services.Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Swashbuckle.AspNetCore.Annotations;

namespace Kollity.Services.API.Controllers;

public class RoomController : BaseController
{
    private readonly IHubContext<RoomHub, IRoomHubClient> _roomHubContext;
    private readonly IRoomConnectionServices _roomConnectionServices;

    public RoomController(IHubContext<RoomHub, IRoomHubClient> roomHubContext,
        IRoomConnectionServices roomConnectionServices)
    {
        _roomHubContext = roomHubContext;
        _roomConnectionServices = roomConnectionServices;
    }

    [HttpGet("{roomId:guid}/student-report/{studentId:guid}"),
     Authorize(Roles = $"{Role.Doctor},{Role.Assistant},{Role.Admin}"),
     SwaggerResponse(200, type: typeof(StudentRoomReportDto))]
    public Task<IResult> StudentReport(Guid roomId, Guid studentId)
    {
        return Send(new GetStudentRoomReportQuery(studentId, roomId));
    }

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
    public Task<IResult> GetMembers(Guid id, [FromQuery] RoomMembersFilterDto dto)
    {
        return Send(new GetRoomMembersQuery(id, dto));
    }

    [HttpGet("{id:guid}/un-read-messages")]
    [SwaggerResponse(200, type: typeof(List<RoomChatMessageDto>))]
    public async Task<IResult> GetUnReadMessages(Guid id)
    {
        var result = await Sender.Send(new GetUnReadMessagesCommand(id));
        if (result.IsSuccess == false)
            return result.ToIResult();

        await _roomHubContext.Clients.Group(id.ToString()).MessagesHaveBeenRead(
            result.Data
                .Where(x => x.IsRead == false)
                .Select(x => x.Id)
                .ToList()
        );

        result.Data.ForEach(x => x.IsRead = false);
        return result.ToIResult();
    }

    [HttpGet("{id:guid}/get-before-date/{date:datetime}")]
    [SwaggerResponse(200, type: typeof(List<RoomChatMessageDto>))]
    public Task<IResult> GetBeforeDate(Guid id, DateTime date)
    {
        return Send(new GetRoomChatMessagesBeforeDateQuery(id, date));
    }

    [HttpGet("poll/{pollId:guid}"),
     SwaggerResponse(200, type: typeof(ChatPollDto))]
    public Task<IResult> GetPoll(Guid pollId)
    {
        return Send(new GetMessageChatPollAnswersQuery(pollId));
    }

    [HttpPost("poll/{pollId:guid}/submit/{optionIndex})")]
    public Task<IResult> SubmitPoll(Guid pollId, byte optionIndex)
    {
        return Send(new SubmitRoomChatMessagePollCommand(pollId, optionIndex));
    }

    [HttpDelete("poll/{pollId:guid}/delete-submission")]
    public Task<IResult> SubmitPoll(Guid pollId)
    {
        return Send(new DeleteRoomChatPollSubmissionCommand(pollId));
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
        return Send(new DenyRoomJoinRequestCommand(new RoomUserIdsMap
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
        return Send(new DeleteRoomSupervisorCommand(new RoomUserIdsMap
        {
            RoomId = roomId,
            UserId = supervisorId
        }));
    }

    [HttpPost("{id:guid}/send-message")]
    public async Task<IResult> SendMessage(Guid id, [FromForm] AddRoomMessageDto dto, Guid trackId)
    {
        var result = await Sender.Send(new AddRoomMessageCommand(id, dto));
        if (result.IsSuccess == false)
            return result.ToIResult();

        var cIds = _roomConnectionServices.GetUserRoomConnectionId(Guid.Parse(Id), id);
        await _roomHubContext.Clients.Clients(cIds).MessageSentSuccessfully(trackId, result.Data);
        await _roomHubContext.Clients.GroupExcept(id.ToString(), cIds).MessageReceived(result.Data);

        return Results.Empty;
    }
}