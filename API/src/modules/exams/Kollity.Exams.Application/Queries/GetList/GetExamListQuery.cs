using Kollity.Exams.Application.Dtos.Exam;

namespace Kollity.Exams.Application.Queries.GetList;

public record GetExamListQuery(Guid RoomId) : IQuery<List<ExamForListDto>>;