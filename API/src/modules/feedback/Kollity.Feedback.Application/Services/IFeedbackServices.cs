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
    Task<Result<Guid>> AddQuestion(AddFeedbackQuestionDto dto, CancellationToken cancellationToken = default);
    Task<Result> DeleteQuestion(Guid id, CancellationToken cancellationToken = default);

    Task<Result<FeedbackStatistics>> GetStatistics(Guid targetId, FeedbackCategory category,
        CancellationToken cancellationToken = default);

    Task<Result<List<FeedbackAnswerDto>>> GetStringAnswersForQuestion(Guid questionId, Guid targetId,
        FeedbackCategory category, int pageIndex, int pageSize, CancellationToken cancellationToken = default);
}