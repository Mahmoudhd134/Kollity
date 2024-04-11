using Kollity.Services.Application.Dtos.Exam;

namespace Kollity.Services.Application.Queries.Exam.GetReview;

public record GetExamReviewQuery(Guid ExamId) : IQuery<ExamForUserReviewDto>;