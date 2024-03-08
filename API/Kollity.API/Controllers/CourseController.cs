using Kollity.Application.Commands.Course.Add;
using Kollity.Application.Commands.Course.AddAssistant;
using Kollity.Application.Commands.Course.AssignDoctor;
using Kollity.Application.Commands.Course.AssignStudent;
using Kollity.Application.Commands.Course.DeAssignDoctor;
using Kollity.Application.Commands.Course.DeAssignStudent;
using Kollity.Application.Commands.Course.Delete;
using Kollity.Application.Commands.Course.DeleteAssistant;
using Kollity.Application.Commands.Course.Edit;
using Kollity.Application.Dtos.Course;
using Kollity.Application.Queries.Course.GetById;
using Kollity.Application.Queries.Course.GetList;
using Kollity.Domain.Identity.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Kollity.API.Controllers;

public class CourseController : BaseController
{
    [HttpPost]
    [Authorize(Roles = $"{Role.Admin}")]
    public Task<IResult> Add(AddCourseDto addCourseDto)
    {
        return Send(new AddCourseCommand(addCourseDto));
    }

    [HttpPatch("assign-doctor")]
    [Authorize(Roles = $"{Role.Admin}")]
    public Task<IResult> AssignDoctor([FromBody] CourseDoctorIdsMap ids)
    {
        return Send(new AssignDoctorToCourseCommand(ids));
    }

    [HttpPost("add-assistant")]
    [Authorize(Roles = $"{Role.Admin}")]
    public Task<IResult> AddAssistant([FromBody] CourseDoctorIdsMap ids)
    {
        return Send(new AddAssistantToCourseCommand(ids));
    }

    [HttpPost("assign-student")]
    [Authorize(Roles = $"{Role.Admin}")]
    public Task<IResult> AssignStudent([FromBody] CourseStudentIdsMap ids)
    {
        return Send(new AssignStudentToCourseCommand(ids));
    }

    [HttpGet("{id:guid}")]
    [SwaggerResponse(200, type: typeof(CourseDto))]
    public Task<IResult> Get(Guid id)
    {
        return Send(new GetCourseByIdQuery(id));
    }

    [HttpGet]
    [SwaggerResponse(200, type: typeof(List<CourseForListDto>))]
    public Task<IResult> Get([FromQuery] CourseListFilters filters)
    {
        return Send(new GetCourseListQuery(filters));
    }

    [HttpPut]
    [Authorize(Roles = $"{Role.Admin}")]
    public Task<IResult> Edit([FromBody] EditCourseDto editCourseDto)
    {
        return Send(new EditCourseCommand(editCourseDto));
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = $"{Role.Admin}")]
    public Task<IResult> Delete(Guid id)
    {
        return Send(new DeleteCourseCommand(id));
    }

    [HttpDelete("{courseId:guid}/deassign-doctor")]
    [Authorize(Roles = $"{Role.Admin}")]
    public Task<IResult> DeAssignDoctor(Guid courseId)
    {
        return Send(new DeAssignDoctorFromCourseCommand(courseId));
    }

    [HttpDelete("{courseId:guid}/delete-assistant/{assistantId:guid}")]
    [Authorize(Roles = $"{Role.Admin}")]
    public Task<IResult> DeleteAssistant(Guid courseId, Guid assistantId)
    {
        return Send(new DeleteAssistantFromCourseCommand(new CourseDoctorIdsMap
        {
            CourseId = courseId,
            DoctorId = assistantId
        }));
    }

    [HttpDelete("{courseId:guid}/deassign-student/{studentId:guid}")]
    [Authorize(Roles = $"{Role.Admin}")]
    public Task<IResult> DeAssignStudent(Guid courseId, Guid studentId)
    {
        return Send(new DeAssignStudentFromCourseCommand(new CourseStudentIdsMap
        {
            CourseId = courseId,
            StudentId = studentId
        }));
    }
}