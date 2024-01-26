using Application.Commands.Course.AddCourse;
using Application.Commands.Course.DeleteCourse;
using Application.Commands.Course.EditCourse;
using Application.Dtos.Course;
using Application.Queries.Course.GetById;
using Application.Queries.Course.GetList;
using Domain.Identity.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

public class CourseController : BaseController
{
    [HttpPost, Authorize(Roles = $"{Role.Admin}")]
    public Task<IResult> Add(AddCourseDto addCourseDto) => Send(new AddCourseCommand(addCourseDto));

    [HttpGet("{id:guid}"), SwaggerResponse(200, type: typeof(CourseDto))]
    public Task<IResult> Get(Guid id) => Send(new GetCourseByIdQuery(id));

    [HttpGet, SwaggerResponse(200, type: typeof(List<CourseForListDto>))]
    public Task<IResult> Get([FromQuery] CourseListFilters filters) => Send(new GetCourseListQuery(filters));

    [HttpPut("{id:guid}"), Authorize(Roles = $"{Role.Admin}")]
    public Task<IResult> Edit(Guid id, [FromBody] EditCourseDto editCourseDto) =>
        Send(new EditCourseCommand(id, editCourseDto));

    [HttpDelete("{id:guid}"), Authorize(Roles = $"{Role.Admin}")]
    public Task<IResult> Delete(Guid id) => Send(new DeleteCourseCommand(id));
}