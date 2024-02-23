using Kollity.API.Extensions;
using Kollity.Application.Commands.Assignment.Add;
using Kollity.Application.Commands.Assignment.AddFile;
using Kollity.Application.Commands.Assignment.Delete;
using Kollity.Application.Commands.Assignment.DeleteFile;
using Kollity.Application.Commands.Assignment.Edit;
using Kollity.Application.Dtos.Assignment;
using Kollity.Application.Dtos.Assignment.Group;
using Kollity.Application.Queries.Assignment.GetById;
using Kollity.Application.Queries.Assignment.GetFile;
using Kollity.Application.Queries.Assignment.GetList;
using Microsoft.AspNetCore.Mvc;

namespace Kollity.API.Controllers;

[Route("api/room/{roomId:guid}/assignment")]
public class AssignmentController : BaseController
{
    [HttpGet]
    public Task<IResult> Get(Guid roomId)
    {
        return Send(new GetAssignmentListQuery(roomId));
    }

    [HttpGet("{assignmentId:guid}")]
    public Task<IResult> GetOne(Guid assignmentId)
    {
        return Send(new GetAssignmentByIdQuery(assignmentId));
    }

    [HttpGet("get-file/{fileId:guid}")]
    public async Task<ActionResult> GetFile(Guid fileId)
    {
        var response = await Sender.Send(new GetAssignmentFileQuery(fileId));
        if (response.IsSuccess == false)
            return response.ToActionResult();
        await CopyFileToResponse(response.Data);
        return new EmptyResult();
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

    [HttpDelete("delete-file/{fileId:guid}")]
    public Task<IResult> DeleteFile(Guid fileId)
    {
        return Send(new DeleteAssignmentFileCommand(fileId));
    }
}