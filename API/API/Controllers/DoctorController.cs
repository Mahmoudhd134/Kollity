using Application.Commands.Doctor.Add;
using Application.Commands.Doctor.Delete;
using Application.Commands.Doctor.Edit;
using Application.Commands.Student.Delete;
using Application.Commands.Student.Edit;
using Application.Dtos.Doctor;
using Application.Dtos.Student;
using Application.Queries.Doctor.GetById;
using Application.Queries.Doctor.GetList;
using Application.Queries.Student.GetById;
using Application.Queries.Student.GetList;
using Domain.Identity.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

public class DoctorController : BaseController
{
    [HttpPost, Authorize(Roles = $"{Role.Admin}")]
    public Task<IResult> Add([FromBody] AddDoctorDto addDoctorDto) => Send(new AddDoctorCommand(addDoctorDto));

    [HttpGet("{id:guid}"), SwaggerResponse(200, type: typeof(DoctorDto))]
    public Task<IResult> Get(Guid id) => Send(new GetDoctorByIdQuery(id));

    [HttpGet, SwaggerResponse(200, type: typeof(List<DoctorForListDto>))]
    public Task<IResult> Get([FromQuery] DoctorListFilters filters) => Send(new GetDoctorListQuery(filters));

    [HttpPut]
    public Task<IResult> Edit([FromBody] EditDoctorDto editDoctorDto) => Send(new EditDoctorCommand(editDoctorDto));

    [HttpDelete("{id:guid}"), Authorize(Roles = $"{Role.Admin}")]
    public Task<IResult> Delete(Guid id) => Send(new DeleteDoctorCommand(id));
}