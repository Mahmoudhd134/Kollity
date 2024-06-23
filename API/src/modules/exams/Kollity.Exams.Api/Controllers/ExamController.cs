using Kollity.Exams.Application.Commands.Add;
using Kollity.Exams.Application.Commands.Delete;
using Kollity.Exams.Application.Commands.Edit;
using Kollity.Exams.Application.Commands.GetNextQuestion;
using Kollity.Exams.Application.Commands.Question.Add;
using Kollity.Exams.Application.Commands.Question.Delete;
using Kollity.Exams.Application.Commands.Question.Edit;
using Kollity.Exams.Application.Commands.Question.Option.Add;
using Kollity.Exams.Application.Commands.Question.Option.Delete;
using Kollity.Exams.Application.Commands.Question.Option.MakeRightOption;
using Kollity.Exams.Application.Commands.Submit;
using Kollity.Exams.Application.Dtos.Exam;
using Kollity.Exams.Application.Queries.GetById;
using Kollity.Exams.Application.Queries.GetDegrees;
using Kollity.Exams.Application.Queries.GetList;
using Kollity.Exams.Application.Queries.GetReview;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Kollity.Exams.Api.Controllers;

[Route("api/room/{roomId:guid}/exam")]
public class ExamController : BaseController
{
    [HttpGet("{examId:guid}")]
    [Authorize(Roles = $"Doctor,Assistant")]
    [SwaggerResponse(200, type: typeof(ExamDto))]
    public Task<IResult> GetById(Guid examId)
    {
        return Send(new GetExamByIdQuery(examId));
    }

    [HttpGet]
    [SwaggerResponse(200, type: typeof(List<ExamForListDto>))]
    public Task<IResult> GetList(Guid roomId)
    {
        return Send(new GetExamListQuery(roomId));
    }

    [HttpGet("{examId:guid}/review")]
    [SwaggerResponse(200, type: typeof(ExamForUserReviewDto))]
    public Task<IResult> GetReview(Guid examId)
    {
        return Send(new GetExamReviewQuery(examId));
    }

    [HttpGet("{examId:guid}/users-degrees")]
    [Authorize(Roles = $"Doctor,Assistant,Admin")]
    [SwaggerResponse(200, type: typeof(ExamDegreesDto))]
    public Task<IResult> GetDegrees(Guid examId, [FromQuery] ExamDegreesFilters filters)
    {
        return Send(new GetExamDegreesQuery(examId, filters));
    }

    [HttpPost("{examId:guid}/get-next-question")]
    [SwaggerResponse(200, type: typeof(ExamQuestionForAnswerDto))]
    public Task<IResult> GetNextQuestion(Guid examId)
    {
        return Send(new GetExamNextQuestionCommand(examId));
    }

    [HttpPost("question/{questionId:guid}/submit-option/{optionId:guid}")]
    public Task<IResult> GetNextQuestion(Guid questionId, Guid optionId)
    {
        return Send(new SubmitExamAnswerCommand(questionId, optionId));
    }

    [HttpPost]
    [Authorize(Roles = $"Doctor,Assistant")]
    [SwaggerResponse(200, type: typeof(Guid))]
    public Task<IResult> Add(Guid roomId, AddExamDto dto)
    {
        return Send(new AddExamCommand(roomId, dto));
    }

    [HttpPost("{examId:guid}/question")]
    [Authorize(Roles = $"Doctor,Assistant")]
    [SwaggerResponse(200, type: typeof(ExamQuestionDto))]
    public Task<IResult> AddQuestion(Guid examId, AddExamQuestionDto dto)
    {
        return Send(new AddExamQuestionCommand(examId, dto));
    }

    [HttpPost("question/{questionId:guid}/option")]
    [Authorize(Roles = $"Doctor,Assistant")]
    [SwaggerResponse(200, type: typeof(Guid))]
    public Task<IResult> AddQuestionOption(Guid questionId, AddExamQuestionOptionDto dto)
    {
        return Send(new AddExamQuestionOptionCommand(questionId, dto));
    }

    [HttpPut]
    [Authorize(Roles = $"Doctor,Assistant")]
    public Task<IResult> Edit(EditExamDto dto)
    {
        return Send(new EditExamCommand(dto));
    }

    [HttpPut("question")]
    [Authorize(Roles = $"Doctor,Assistant")]
    public Task<IResult> EditQuestion(EditExamQuestionDto dto)
    {
        return Send(new EditExamQuestionCommand(dto));
    }

    [HttpPatch("question/option/{optionId:guid}/make-right-option")]
    [Authorize(Roles = $"Doctor,Assistant")]
    public Task<IResult> MakeOptionRightOption(Guid optionId)
    {
        return Send(new MakeExamQuestionOptionIsTheRightOptionCommand(optionId));
    }


    [HttpDelete("{examId:guid}")]
    [Authorize(Roles = $"Doctor,Assistant")]
    public Task<IResult> Delete(Guid examId)
    {
        return Send(new DeleteExamCommand(examId));
    }

    [HttpDelete("question/{questionId:guid}")]
    [Authorize(Roles = $"Doctor,Assistant")]
    public Task<IResult> DeleteQuestion(Guid questionId)
    {
        return Send(new DeleteExamQuestionCommand(questionId));
    }

    [HttpDelete("question/option/{optionId:guid}")]
    [Authorize(Roles = $"Doctor,Assistant")]
    public Task<IResult> DeleteQuestionOption(Guid optionId)
    {
        return Send(new DeleteExamQuestionOptionCommand(optionId));
    }
}