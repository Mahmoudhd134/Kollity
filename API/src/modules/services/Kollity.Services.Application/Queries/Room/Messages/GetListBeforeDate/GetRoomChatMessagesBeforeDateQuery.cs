using Kollity.Services.Application.Dtos.Room.Message;

namespace Kollity.Services.Application.Queries.Room.Messages.GetListBeforeDate;

public record GetRoomChatMessagesBeforeDateQuery(Guid RoomId, DateTime Date) : IQuery<List<RoomChatMessageDto>>;