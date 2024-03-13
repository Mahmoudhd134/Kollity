using Kollity.Application.Dtos.Exam;

namespace Kollity.Application.Queries.Exam.GetReview;

public record GetExamReviewQuery(Guid ExamId) : IQuery<ExamForUserReviewDto>;