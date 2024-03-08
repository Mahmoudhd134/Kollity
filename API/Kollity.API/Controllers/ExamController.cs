using Kollity.Application.Commands.Exam.Add;
using Kollity.Application.Commands.Exam.Delete;
using Kollity.Application.Commands.Exam.Edit;
using Kollity.Application.Dtos.Exam;
using Kollity.Application.Queries.Exam.GetList;
using Kollity.Domain.Identity.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Kollity.API.Controllers;

[Route("api/room/{roomId:guid}/exam")]
public class ExamController : BaseController
{
    [HttpPost, Authorize(Roles = $"{Role.Doctor},{Role.Assistant}")]
    public Task<IResult> Add(Guid roomId, AddExamDto dto)
    {
        return Send(new AddExamCommand(roomId, dto));
    }

    [HttpGet, SwaggerResponse(200, type: typeof(List<ExamForListDto>))]
    public Task<IResult> GetList(Guid roomId)
    {
        return Send(new GetExamListQuery(roomId));
    }

    [HttpPut, Authorize(Roles = $"{Role.Doctor},{Role.Assistant}")]
    public Task<IResult> Edit(EditExamDto dto)
    {
        return Send(new EditExamCommand(dto));
    }

    [HttpDelete("{examId:guid}"), Authorize(Roles = $"{Role.Doctor},{Role.Assistant}")]
    public Task<IResult> Delete(Guid examId)
    {
        return Send(new DeleteExamCommand(examId));
    }
}