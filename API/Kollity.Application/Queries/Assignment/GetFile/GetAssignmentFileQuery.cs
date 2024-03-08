using Kollity.Application.Dtos;

namespace Kollity.Application.Queries.Assignment.GetFile;

public record GetAssignmentFileQuery(Guid FileId) : IQuery<FileStreamDto>;