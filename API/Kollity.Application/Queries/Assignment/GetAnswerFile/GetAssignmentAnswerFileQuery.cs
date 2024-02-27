using Kollity.Application.Dtos;

namespace Kollity.Application.Queries.Assignment.GetAnswerFile;

public record GetAssignmentAnswerFileQuery(Guid AnswerId) : IQuery<FileStreamDto>;