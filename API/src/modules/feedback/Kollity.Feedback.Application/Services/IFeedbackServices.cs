using Kollity.Common.ErrorHandling;
using Kollity.Feedback.Application.Dtos;
using Kollity.Feedback.Domain.FeedbackModels;

namespace Kollity.Feedback.Application.Services;

public interface IFeedbackServices
{
    Task<Result<List<FeedbackQuestionDto>>> GetAllQuestions(FeedbackCategory category,
        CancellationToken cancellationToken = default);

    Task<Result> AnswerFeedbacks(FeedbacksAnswerDto answers, CancellationToken cancellationToken = default);
    Task<Result<List<FeedbackAvailableCategory>>> AvailableFeedbacks(CancellationToken cancellationToken = default);
}