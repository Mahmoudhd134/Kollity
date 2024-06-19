using Kollity.Feedback.Domain.FeedbackModels;

namespace Kollity.Feedback.Application.Dtos;

public class FeedbackAvailableCategory
{
    public FeedbackCategory Category { get; set; }
    public List<IdNameMap> Available { get; set; }
}