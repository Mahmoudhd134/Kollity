using Kollity.Services.Application.Dtos.Exam;

namespace Kollity.Services.Application.Queries.Exam.GetList;

public record GetExamListQuery(Guid RoomId) : IQuery<List<ExamForListDto>>;