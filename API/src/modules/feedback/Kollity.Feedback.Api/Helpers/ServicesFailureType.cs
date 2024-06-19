using Kollity.Common.ErrorHandling;

namespace Kollity.Feedback.Api.Helpers;

public class FeedbackFailureType
{
    public string Type { get; set; }
    public string Title { get; set; }
    public int Status { get; set; }
    public List<Error> Errors { get; set; }
}