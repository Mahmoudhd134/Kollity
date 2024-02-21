using Kollity.Application.Dtos;

namespace Kollity.Application.Queries.Room.GetSingleContent;

public record GetRoomSingleContentQuery(Guid ContentId) : IQuery<FileStreamDto>;