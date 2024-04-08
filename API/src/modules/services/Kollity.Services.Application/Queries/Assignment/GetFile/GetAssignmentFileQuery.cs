using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Dtos;

namespace Kollity.Services.Application.Queries.Assignment.GetFile;

public record GetAssignmentFileQuery(Guid FileId) : IQuery<FileStreamDto>;