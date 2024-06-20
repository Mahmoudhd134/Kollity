using System.ComponentModel.DataAnnotations;
using Kollity.Feedback.Domain.FeedbackModels;

namespace Kollity.Feedback.Application.Dtos;

public class AddFeedbackQuestionDto
{
    [Required]
    public string Question { get; set; }
    public bool IsMcq { get; set; }
    public FeedbackCategory Category { get; set; }
}