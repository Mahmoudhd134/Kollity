using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Dtos;

namespace Kollity.Services.Application.Queries.Room.GetSingleContent;

public record GetRoomSingleContentQuery(Guid ContentId) : IQuery<FileStreamDto>;