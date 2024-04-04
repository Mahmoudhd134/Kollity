using Kollity.API.Extensions;
using Kollity.API.Hubs.Abstraction;
using Kollity.Application.Commands.Room.Messages.Add;
using Kollity.Application.Commands.Room.Messages.Delete;
using Kollity.Application.Commands.Room.Messages.Disconnect;
using Kollity.Application.Dtos.Room.Message;
using Kollity.Domain.ErrorHandlers.Abstractions;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Primitives;
using SignalRSwaggerGen.Attributes;

namespace Kollity.API.Hubs.Hubs.Room;

[SignalRHub]
public class RoomHub : BaseHub<IRoomHubClient>
{
    private readonly IRoomConnectionServices _roomConnectionServices;

    public RoomHub(IRoomConnectionServices roomConnectionServices)
    {
        _roomConnectionServices = roomConnectionServices;
    }

    public override async Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        var roomId = new StringValues();
        var foundRoomId = httpContext?.Request.Query.TryGetValue("roomId", out roomId);
        if ((foundRoomId ?? false) == false)
            return;
        await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
        _roomConnectionServices.AddConnection(
            Context.ConnectionId,
            UserServices.GetCurrentUserId(),
            Guid.Parse(roomId)
        );
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        var roomId = _roomConnectionServices.GetConnectionRoomId(Context.ConnectionId);
        if (roomId == Guid.Empty)
            return;
        await Sender.Send(new UserDisconnectRoomCommand(roomId));
        _roomConnectionServices.RemoveConnection(Context.ConnectionId);
        await base.OnDisconnectedAsync(exception);
    }

    public async Task SendMessage(Guid trackId, string text, IFormFile file, Guid roomId)
    {
        var result = await Sender.Send(new AddRoomMessageCommand(roomId, new AddRoomMessageDto
        {
            Text = text,
            File = file
        }));

        if (result.IsSuccess == false)
        {
            await Clients.Caller.MessageHasNotBeenSentSuccessfully(trackId, result.Errors);
            return;
        }

        await Clients.Caller.MessageSentSuccessfully(trackId, result.Data);
        await Clients.OthersInGroup(roomId.ToString()).MessageReceived(result.Data);
    }

    public async Task DeleteMessage(Guid messageId, Guid roomId)
    {
        var result = await Sender.Send(new DeleteRoomChatMessageCommand(messageId));
        if (result.IsSuccess == false)
        {
            await Clients.Caller.MessageHasNotBeenDeletedSuccessfully(messageId, result.Errors);
            return;
        }

        await Clients.Group(roomId.ToString()).MessageDeleted(messageId);
    }
}