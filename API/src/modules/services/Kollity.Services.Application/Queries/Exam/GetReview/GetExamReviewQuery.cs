using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Dtos.Exam;

namespace Kollity.Services.Application.Queries.Exam.GetReview;

public record GetExamReviewQuery(Guid ExamId) : IQuery<ExamForUserReviewDto>;