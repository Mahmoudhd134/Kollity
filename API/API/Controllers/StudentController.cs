using Application.Commands.Student.AddStudent;
using Application.Commands.Student.DeleteStudent;
using Application.Commands.Student.EditStudent;
using Application.Dtos.Student;
using Application.Queries.Student.GetById;
using Application.Queries.Student.GetList;
using Domain.Identity.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

public class StudentController : BaseController
{
    [HttpPost, Authorize(Roles = $"{Role.Admin}")]
    public Task<IResult> Add(AddStudentDto addStudentDto) => Send(new AddStudentCommand(addStudentDto));

    [HttpGet("{id:guid}"), SwaggerResponse(200, type: typeof(StudentDto))]
    public Task<IResult> Get(Guid id) => Send(new GetStudentByIdQuery(id));

    [HttpGet, SwaggerResponse(200, type: typeof(List<StudentDto>))]
    public Task<IResult> Get([FromQuery] StudentListFilters filters) => Send(new GetStudentListQuery(filters));

    [HttpPut]
    public Task<IResult> Edit([FromBody] EditStudentDto editStudentDto) =>
        Send(new EditStudentCommand(Guid.Parse(Id), editStudentDto));

    [HttpDelete("{id:guid}"), Authorize(Roles = $"{Role.Admin}")]
    public Task<IResult> Delete(Guid id) => Send(new DeleteStudentCommand(id));
}