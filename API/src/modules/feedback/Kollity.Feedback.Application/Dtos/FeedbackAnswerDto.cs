using Kollity.Feedback.Domain.FeedbackModels;

namespace Kollity.Feedback.Application.Dtos;

public class FeedbackAnswerDto
{
    public Guid QuestionId { get; set; }
    public FeedbackQuestionAnswer? Answer { get; set; }
    public string StringAnswer { get; set; }
}