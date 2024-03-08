using Kollity.Application.Dtos.Exam;

namespace Kollity.Application.Queries.Exam.GetList;

public record GetExamListQuery(Guid RoomId) : IQuery<List<ExamForListDto>>;