using Kollity.Feedback.Domain.FeedbackModels;

namespace Kollity.Feedback.Application.Dtos;

public class FeedbackQuestionDto
{
    public Guid Id { get; set; }
    public string Question { get; set; }
    public bool IsMcq { get; set; }
    public FeedbackCategory Category { get; set; }
}