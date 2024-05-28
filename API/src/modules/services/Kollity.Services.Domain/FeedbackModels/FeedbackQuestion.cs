using Kollity.Services.Domain.Identity;

namespace Kollity.Services.Domain.FeedbackModels;

public class FeedbackQuestion
{
    public Guid Id { get; set; }
    public string Question { get; set; }
    public bool IsMcq { get; set; }
    public FeedbackCategory Category { get; set; }
}