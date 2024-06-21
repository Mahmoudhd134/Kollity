using Kollity.Feedback.Domain.FeedbackModels;

namespace Kollity.Feedback.Application.Dtos;

public class FeedbackStatisticsQuestionOptionDto
{
    public FeedbackQuestionAnswer Answer { get; set; }
    public int Count { get; set; }
}