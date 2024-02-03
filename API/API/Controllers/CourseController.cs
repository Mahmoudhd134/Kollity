using Application.Commands.Course.Add;
using Application.Commands.Course.AddAssistant;
using Application.Commands.Course.AssignDoctor;
using Application.Commands.Course.AssignStudent;
using Application.Commands.Course.DeAssignDoctor;
using Application.Commands.Course.DeAssignStudent;
using Application.Commands.Course.Delete;
using Application.Commands.Course.DeleteAssistant;
using Application.Commands.Course.Edit;
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

    [HttpPost("assign-doctor"), Authorize(Roles = $"{Role.Admin}")]
    public Task<IResult> AssignDoctor([FromBody] CourseDoctorIdsMap ids) => Send(new AssignDoctorToCourseCommand(ids));

    [HttpPost("add-assistant"), Authorize(Roles = $"{Role.Admin}")]
    public Task<IResult> AddAssistant([FromBody] CourseDoctorIdsMap ids) => Send(new AddAssistantToCourseCommand(ids));

    [HttpPost("assign-student"), Authorize(Roles = $"{Role.Admin}")]
    public Task<IResult> AssignStudent([FromBody] CourseStudentIdsMap ids) =>
        Send(new AssignStudentToCourseCommand(ids));

    [HttpGet("{id:guid}"), SwaggerResponse(200, type: typeof(CourseDto))]
    public Task<IResult> Get(Guid id) => Send(new GetCourseByIdQuery(id));

    [HttpGet, SwaggerResponse(200, type: typeof(List<CourseForListDto>))]
    public Task<IResult> Get([FromQuery] CourseListFilters filters) => Send(new GetCourseListQuery(filters));

    [HttpPut, Authorize(Roles = $"{Role.Admin}")]
    public Task<IResult> Edit([FromBody] EditCourseDto editCourseDto) =>
        Send(new EditCourseCommand(editCourseDto));

    [HttpDelete("{id:guid}"), Authorize(Roles = $"{Role.Admin}")]
    public Task<IResult> Delete(Guid id) => Send(new DeleteCourseCommand(id));

    [HttpDelete("deassign-doctor/{courseId:guid}"), Authorize(Roles = $"{Role.Admin}")]
    public Task<IResult> DeAssignDoctor(Guid courseId) => Send(new DeAssignDoctorFromCourseCommand(courseId));

    [HttpDelete("delete-assistant"), Authorize(Roles = $"{Role.Admin}")]
    public Task<IResult> DeleteAssistant([FromBody] CourseDoctorIdsMap ids) =>
        Send(new DeleteAssistantFromCourseCommand(ids));

    [HttpDelete("deassign-student"), Authorize(Roles = $"{Role.Admin}")]
    public Task<IResult> DeAssignStudent([FromBody] CourseStudentIdsMap ids) =>
        Send(new DeAssignStudentFromCourseCommand(ids));
}