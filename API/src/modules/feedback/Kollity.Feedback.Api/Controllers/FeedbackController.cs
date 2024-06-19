using Kollity.Feedback.Api.Extensions;
using Kollity.Feedback.Application.Dtos;
using Kollity.Feedback.Application.Services;
using Kollity.Feedback.Domain.FeedbackModels;
using Kollity.Feedback.Persistence.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace Kollity.Feedback.Api.Controllers;

[Route("api/feedback")]
public class FeedbackController(FeedbackDbContext context, IFeedbackServices feedbackServices) : BaseController
{
    [HttpGet("available")]
    public async Task<IResult> GetAvailable(CancellationToken cancellationToken)
    {
        return (await feedbackServices.AvailableFeedbacks(cancellationToken)).ToIResult();
    }

    [HttpPost("answer")]
    public async Task<IResult> Answer(FeedbacksAnswerDto answers, CancellationToken cancellationToken)
    {
        return (await feedbackServices.AnswerFeedbacks(answers, cancellationToken)).ToIResult();
    }

    [HttpGet("questions")]
    public async Task<IResult> Answer(FeedbackCategory category, CancellationToken cancellationToken)
    {
        return (await feedbackServices.GetAllQuestions(category, cancellationToken)).ToIResult();
    }
}