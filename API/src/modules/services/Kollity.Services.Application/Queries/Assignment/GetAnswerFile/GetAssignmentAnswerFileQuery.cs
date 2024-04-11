using Kollity.Services.Application.Dtos;

namespace Kollity.Services.Application.Queries.Assignment.GetAnswerFile;

public record GetAssignmentAnswerFileQuery(Guid AnswerId) : IQuery<FileStreamDto>;