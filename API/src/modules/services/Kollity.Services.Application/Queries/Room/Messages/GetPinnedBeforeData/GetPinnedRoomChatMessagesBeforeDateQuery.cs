using Kollity.Services.Application.Dtos.Room.Message;

namespace Kollity.Services.Application.Queries.Room.Messages.GetPinnedBeforeData;

public record GetPinnedRoomChatMessagesBeforeDateQuery(Guid RoomId, DateTime Date,int Count) : IQuery<List<RoomChatMessageDto>>;