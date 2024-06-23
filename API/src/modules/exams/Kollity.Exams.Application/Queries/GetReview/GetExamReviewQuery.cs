using Kollity.Exams.Application.Dtos.Exam;

namespace Kollity.Exams.Application.Queries.GetReview;

public record GetExamReviewQuery(Guid ExamId) : IQuery<ExamForUserReviewDto>;