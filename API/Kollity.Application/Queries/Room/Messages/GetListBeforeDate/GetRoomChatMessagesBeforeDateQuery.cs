using Kollity.Application.Dtos.Room.Message;

namespace Kollity.Application.Queries.Room.Messages.GetListBeforeDate;

public record GetRoomChatMessagesBeforeDateQuery(Guid RoomId, DateTime Date) : IQuery<List<RoomChatMessageDto>>;