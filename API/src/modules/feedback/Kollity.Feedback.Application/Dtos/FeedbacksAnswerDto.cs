using Kollity.Feedback.Domain.FeedbackModels;

namespace Kollity.Feedback.Application.Dtos;

public class FeedbacksAnswerDto
{
    public Guid TargetId { get; set; }
    public FeedbackCategory Category { get; set; }
    public List<FeedbackAnswerDto> Answers { get; set; }
}