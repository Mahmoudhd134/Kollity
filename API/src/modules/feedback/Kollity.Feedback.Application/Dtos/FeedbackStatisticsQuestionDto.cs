namespace Kollity.Feedback.Application.Dtos;

public class FeedbackStatisticsQuestionDto
{
    public Guid Id { get; set; }
    public string Question { get; set; }
    public List<FeedbackStatisticsQuestionOptionDto> Options { get; set; }
}