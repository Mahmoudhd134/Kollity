using Kollity.Application.Commands.Student.Add;
using Kollity.Application.Commands.Student.Delete;
using Kollity.Application.Commands.Student.Edit;
using Kollity.Application.Dtos.Student;
using Kollity.Application.Queries.Student.GetById;
using Kollity.Application.Queries.Student.GetList;
using Kollity.Domain.Identity.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Kollity.API.Controllers;

public class StudentController : BaseController
{
    [HttpPost]
    [Authorize(Roles = $"{Role.Admin}")]
    public Task<IResult> Add(AddStudentDto addStudentDto)
    {
        return Send(new AddStudentCommand(addStudentDto));
    }

    [HttpGet("{id:guid}")]
    [SwaggerResponse(200, type: typeof(StudentDto))]
    public Task<IResult> Get(Guid id)
    {
        return Send(new GetStudentByIdQuery(id));
    }

    [HttpGet]
    [SwaggerResponse(200, type: typeof(List<StudentForListDto>))]
    public Task<IResult> Get([FromQuery] StudentListFilters filters)
    {
        return Send(new GetStudentListQuery(filters));
    }

    [HttpPut]
    public Task<IResult> Edit([FromBody] EditStudentDto editStudentDto)
    {
        return Send(new EditStudentCommand(editStudentDto));
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = $"{Role.Admin}")]
    public Task<IResult> Delete(Guid id)
    {
        return Send(new DeleteStudentCommand(id));
    }
}