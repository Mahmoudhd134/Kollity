using System.Globalization;
using Kollity.API.Extensions;
using Kollity.Application.Commands.Room.AddContent;
using Kollity.Application.Commands.Room.DeleteContent;
using Kollity.Application.Dtos.Room;
using Kollity.Application.Queries.Room.GetContent;
using Kollity.Application.Queries.Room.GetSingleContent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Kollity.API.Controllers;

[Route(("api/room/{id:guid}/content"))]
public class RoomContentController : BaseController
{
    [HttpPost]
    [RequestSizeLimit(MaxFileSize)]
    [RequestFormLimits(MultipartBodyLengthLimit = MaxFileSize)]
    public Task<IResult> AddContent(Guid id, [FromForm] AddRoomContentDto addRoomContentDto)
    {
        return Send(new AddRoomContentCommand(id, addRoomContentDto));
    }

    [HttpGet, SwaggerResponse(200, type: typeof(List<RoomContentDto>))]
    public Task<IResult> GetContent(Guid id)
    {
        return Send(new GetRoomContentQuery(id));
    }

    [AllowAnonymous, HttpGet("{contentId:guid}"), SwaggerResponse(200, type: typeof(File))]
    public async Task<ActionResult> GetSingleContent(Guid id, Guid contentId)
    {
        var response = await Sender.Send(new GetRoomSingleContentQuery(contentId));
        if (response.IsSuccess == false)
            return response.ToActionResult();

        Response.Headers.Append("Content-Disposition",
            "attachment; filename=" + response.Data.Name + response.Data.Extension);
        Response.Headers.Append("Content-Type", "application/octet-stream");
        Response.Headers.Append("Content-Length", response.Data.Size.ToString(CultureInfo.InvariantCulture));
        await response.Data.Stream.CopyToAsync(Response.Body);
        response.Data.Stream.Close();
        return new EmptyResult();
    }

    [HttpDelete("{contentId:guid}")]
    public Task<IResult> DeleteContent(Guid id, Guid contentId)
    {
        return Send(new DeleteRoomContentCommand(id, contentId));
    }
}