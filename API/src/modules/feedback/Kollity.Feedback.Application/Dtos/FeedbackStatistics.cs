using Kollity.Feedback.Domain.FeedbackModels;

namespace Kollity.Feedback.Application.Dtos;

public class FeedbackStatistics
{
    public FeedbackCategory Category { get; set; }
    public Guid TargetId { get; set; }
    public string TargetName { get; set; }
    public int TotalParticipants { get; set; }
    public List<FeedbackStatisticsQuestionDto> Questions { get; set; }
}