using System.ComponentModel.DataAnnotations;
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
    [HttpGet("available"), SwaggerResponse(200, type: typeof(List<FeedbackAvailableCategory>))]
    public async Task<IResult> GetAvailable(CancellationToken cancellationToken)
    {
        return (await feedbackServices.AvailableFeedbacks(cancellationToken)).ToIResult();
    }

    [HttpPost("answer")]
    public async Task<IResult> Answer(FeedbacksAnswerDto answers, CancellationToken cancellationToken)
    {
        return (await feedbackServices.AnswerFeedbacks(answers, cancellationToken)).ToIResult();
    }

    [HttpPost("questions"), Authorize(Roles = "Admin"), SwaggerResponse(200, type: typeof(Guid))]
    public async Task<IResult> AddQuestion([FromBody] AddFeedbackQuestionDto dto, CancellationToken cancellationToken)
    {
        return (await feedbackServices.AddQuestion(dto, cancellationToken)).ToIResult();
    }

    [HttpDelete("questions/{id:guid}"), Authorize(Roles = "Admin")]
    public async Task<IResult> AddQuestion(Guid id, CancellationToken cancellationToken)
    {
        return (await feedbackServices.DeleteQuestion(id, cancellationToken)).ToIResult();
    }

    [HttpGet("questions/{category}"), SwaggerResponse(200, type: typeof(List<FeedbackQuestionDto>))]
    public async Task<IResult> Answer(FeedbackCategory category, CancellationToken cancellationToken)
    {
        return (await feedbackServices.GetAllQuestions(category, cancellationToken)).ToIResult();
    }

    [HttpGet("statistics/{targetId:guid}/{category}"),
     Authorize(Roles = "Admin,Doctor,Assistant"),
     SwaggerResponse(200, type: typeof(FeedbackStatistics))]
    public async Task<IResult> Statistics(Guid targetId, FeedbackCategory category, CancellationToken cancellationToken)
    {
        return (await feedbackServices.GetStatistics(targetId, category, cancellationToken)).ToIResult();
    }

    [HttpGet("not-mcq-answers/{questionId:guid}/{targetId:guid}/{category}"),
     Authorize(Roles = "Admin,Doctor,Assistant"),
     SwaggerResponse(200, type: typeof(List<FeedbackAnswerDto>))]
    public async Task<IResult> Statistics(
        Guid questionId,
        Guid targetId,
        FeedbackCategory category,
        [Required] int pageIndex,
        [Required] int pageSize,
        CancellationToken cancellationToken)
    {
        return (await feedbackServices.GetStringAnswersForQuestion(questionId, targetId, category, pageIndex, pageSize,
            cancellationToken)).ToIResult();
    }
}