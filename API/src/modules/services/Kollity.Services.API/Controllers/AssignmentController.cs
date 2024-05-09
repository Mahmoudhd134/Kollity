using Kollity.Common;
using Kollity.Services.API.Extensions;
using Kollity.Services.Application.Abstractions.Services;
using Kollity.Services.Application.Commands.Assignment.Add;
using Kollity.Services.Application.Commands.Assignment.AddFile;
using Kollity.Services.Application.Commands.Assignment.Answer;
using Kollity.Services.Application.Commands.Assignment.Delete;
using Kollity.Services.Application.Commands.Assignment.DeleteAnswer;
using Kollity.Services.Application.Commands.Assignment.DeleteFile;
using Kollity.Services.Application.Commands.Assignment.Edit;
using Kollity.Services.Application.Commands.Assignment.SetDegree;
using Kollity.Services.Application.Dtos;
using Kollity.Services.Application.Dtos.Assignment;
using Kollity.Services.Application.Dtos.Assignment.Group;
using Kollity.Services.Application.Events.Doctor;
using Kollity.Services.Application.Events.Student;
using Kollity.Services.Application.Queries.Assignment.GetAnswerFile;
using Kollity.Services.Application.Queries.Assignment.GetById;
using Kollity.Services.Application.Queries.Assignment.GetFile;
using Kollity.Services.Application.Queries.Assignment.GetGroupAnswers;
using Kollity.Services.Application.Queries.Assignment.GetGroupDegree;
using Kollity.Services.Application.Queries.Assignment.GetIndividualAnswers;
using Kollity.Services.Application.Queries.Assignment.GetList;
using Kollity.Services.Application.Queries.Assignment.Report;
using Kollity.Services.Domain.Identity;
using Kollity.Services.Persistence.Data;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace Kollity.Services.API.Controllers;

[Route("api/room/{roomId:guid}/assignment")]
public class AssignmentController : BaseController
{
    [HttpGet]
    [SwaggerResponse(200, type: typeof(List<AssignmentForListDto>))]
    public Task<IResult> Get(Guid roomId)
    {
        return Send(new GetAssignmentListQuery(roomId));
    }

    [HttpGet("{assignmentId:guid}")]
    [SwaggerResponse(200, type: typeof(AssignmentDto))]
    public Task<IResult> GetOne(Guid assignmentId)
    {
        return Send(new GetAssignmentByIdQuery(assignmentId));
    }

    [HttpGet("get-file/{fileId:guid}")]
    [AllowAnonymous]
    public async Task<ActionResult> GetFile(Guid fileId)
    {
        var response = await Sender.Send(new GetAssignmentFileQuery(fileId));
        if (response.IsSuccess == false)
            return response.ToActionResult();
        await CopyFileToResponse(response.Data);
        return new EmptyResult();
    }

    [HttpGet("{assignmentId:guid}/group-answers/{groupId:guid}")]
    [Authorize(Roles = $"{Role.Admin},{Role.Doctor},{Role.Assistant}")]
    [SwaggerResponse(200, type: typeof(AssignmentGroupDegreeDto))]
    public Task<IResult> GetGroupMembersDegrees(Guid assignmentId, Guid groupId)
    {
        return Send(new GetAssignmentGroupMembersAnswerDegreeQuery(assignmentId, groupId));
    }

    [HttpGet("{assignmentId:guid}/group-answers")]
    [Authorize(Roles = $"{Role.Admin},{Role.Doctor},{Role.Assistant}")]
    [SwaggerResponse(200, type: typeof(GroupingAssignmentAnswersDto))]
    public Task<IResult> GetGroupAnswers(Guid assignmentId, [FromQuery] GroupAssignmentAnswersFilters filters)
    {
        return Send(new GetGroupingAssignmentAnswersQuery(assignmentId, filters));
    }

    [HttpGet("{assignmentId:guid}/individual-answers")]
    [Authorize(Roles = $"{Role.Admin},{Role.Doctor},{Role.Assistant}")]
    [SwaggerResponse(200, type: typeof(IndividualAssignmentAnswersDto))]
    public Task<IResult> GetIndividualAnswers(Guid assignmentId, [FromQuery] IndividualAssignmentAnswersFilters filters)
    {
        return Send(new GetAssignmentIndividualAnswersQuery(assignmentId, filters));
    }

    [HttpGet("{assignmentId:guid}/report")]
    [Authorize(Roles = $"{Role.Admin},{Role.Doctor},{Role.Assistant}")]
    [SwaggerResponse(200, type: typeof(AssignmentReportDto))]
    public Task<IResult> GetReport(Guid assignmentId, [FromQuery] PaginationDto paginationDto)
    {
        return Send(new GetAssignmentReportQuery(assignmentId, paginationDto));
    }

    [HttpGet("answer-file/{answerId:guid}")]
    [AllowAnonymous]
    public async Task<ActionResult> GetAnswerFile(Guid answerId)
    {
        var response = await Sender.Send(new GetAssignmentAnswerFileQuery(answerId));
        if (response.IsSuccess == false)
            return response.ToActionResult();
        await CopyFileToResponse(response.Data);
        return new EmptyResult();
    }

    [HttpPost("answer/set-student-degree")]
    public Task<IResult> SetStudentDegree(SetAnswerDegreeDto dto)
    {
        return Send(new SetStudentAnswerDegreeCommand(dto));
    }

    [HttpPost("{assignmentId:guid}/submit-answer")]
    public Task<IResult> SubmitAnswer(Guid assignmentId, [FromForm] AddAssignmentAnswerDto dto)
    {
        return Send(new AddAssignmentAnswerCommand(assignmentId, dto));
    }

    [HttpPost]
    public Task<IResult> Add(Guid roomId, AddAssignmentDto dto)
    {
        return Send(new AddAssignmentCommand(roomId, dto));
    }

    [HttpPost("{assignmentId:guid}/add-file")]
    public Task<IResult> AddFile(Guid assignmentId, AddAssignmentFileDto dto)
    {
        return Send(new AddAssignmentFileCommand(assignmentId, dto));
    }

    [HttpPut]
    public Task<IResult> Edit(EditAssignmentDto dto)
    {
        return Send(new EditAssignmentCommand(dto));
    }

    [HttpDelete("{assignmentId:guid}")]
    public Task<IResult> Delete(Guid assignmentId)
    {
        return Send(new DeleteAssignmentCommand(assignmentId));
    }

    [HttpDelete("{assignmentId:guid}/delete-answer")]
    public Task<IResult> DeleteAnswer(Guid assignmentId)
    {
        return Send(new DeleteAssignmentAnswerCommand(assignmentId));
    }

    [HttpDelete("delete-file/{fileId:guid}")]
    public Task<IResult> DeleteFile(Guid fileId)
    {
        return Send(new DeleteAssignmentFileCommand(fileId));
    }
}