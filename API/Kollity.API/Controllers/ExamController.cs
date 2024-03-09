using Kollity.Application.Commands.Exam.Add;
using Kollity.Application.Commands.Exam.Delete;
using Kollity.Application.Commands.Exam.Edit;
using Kollity.Application.Commands.Exam.Question.Add;
using Kollity.Application.Commands.Exam.Question.Delete;
using Kollity.Application.Commands.Exam.Question.Edit;
using Kollity.Application.Commands.Exam.Question.Option.Add;
using Kollity.Application.Commands.Exam.Question.Option.Delete;
using Kollity.Application.Commands.Exam.Question.Option.MakeRightOption;
using Kollity.Application.Dtos.Exam;
using Kollity.Application.Queries.Exam.GetById;
using Kollity.Application.Queries.Exam.GetList;
using Kollity.Domain.Identity.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Kollity.API.Controllers;

[Route("api/room/{roomId:guid}/exam")]
public class ExamController : BaseController
{
    [HttpGet("{examId:guid}"),
     Authorize(Roles = $"{Role.Doctor},{Role.Assistant}"),
     SwaggerResponse(200, type: typeof(ExamDto))]
    public Task<IResult> GetById(Guid examId)
    {
        return Send(new GetExamByIdQuery(examId));
    }

    [HttpGet, SwaggerResponse(200, type: typeof(List<ExamForListDto>))]
    public Task<IResult> GetList(Guid roomId)
    {
        return Send(new GetExamListQuery(roomId));
    }

    [HttpPost,
     Authorize(Roles = $"{Role.Doctor},{Role.Assistant}"),
     SwaggerResponse(200, type: typeof(Guid))]
    public Task<IResult> Add(Guid roomId, AddExamDto dto)
    {
        return Send(new AddExamCommand(roomId, dto));
    }

    [HttpPost("{examId:guid}/add-question"),
     Authorize(Roles = $"{Role.Doctor},{Role.Assistant}"),
     SwaggerResponse(200, type: typeof(Guid))]
    public Task<IResult> AddQuestion(Guid examId, AddExamQuestionDto dto)
    {
        return Send(new AddExamQuestionCommand(examId, dto));
    }

    [HttpPost("question/{questionId:guid}/add-option"),
     Authorize(Roles = $"{Role.Doctor},{Role.Assistant}"),
     SwaggerResponse(200, type: typeof(Guid))]
    public Task<IResult> AddQuestionOption(Guid questionId, AddExamQuestionOptionDto dto)
    {
        return Send(new AddExamQuestionOptionCommand(questionId, dto));
    }

    [HttpPut, Authorize(Roles = $"{Role.Doctor},{Role.Assistant}")]
    public Task<IResult> Edit(EditExamDto dto)
    {
        return Send(new EditExamCommand(dto));
    }

    [HttpPut("edit-question"), Authorize(Roles = $"{Role.Doctor},{Role.Assistant}")]
    public Task<IResult> EditQuestion(EditExamQuestionDto dto)
    {
        return Send(new EditExamQuestionCommand(dto));
    }

    [HttpPatch("question/option/{optionId:guid}/make-right-option"),
     Authorize(Roles = $"{Role.Doctor},{Role.Assistant}")]
    public Task<IResult> MakeOptionRightOption(Guid optionId)
    {
        return Send(new MakeExamQuestionOptionIsTheRightOptionCommand(optionId));
    }


    [HttpDelete("{examId:guid}"), Authorize(Roles = $"{Role.Doctor},{Role.Assistant}")]
    public Task<IResult> Delete(Guid examId)
    {
        return Send(new DeleteExamCommand(examId));
    }

    [HttpDelete("delete-question/{questionId:guid}"), Authorize(Roles = $"{Role.Doctor},{Role.Assistant}")]
    public Task<IResult> DeleteQuestion(Guid questionId)
    {
        return Send(new DeleteExamQuestionCommand(questionId));
    }

    [HttpDelete("question/option/{optionId:guid}"), Authorize(Roles = $"{Role.Doctor},{Role.Assistant}")]
    public Task<IResult> DeleteQuestionOption(Guid optionId)
    {
        return Send(new DeleteExamQuestionOptionCommand(optionId));
    }
}