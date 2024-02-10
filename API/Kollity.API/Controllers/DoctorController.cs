using Kollity.Application.Commands.Doctor.Add;
using Kollity.Application.Commands.Doctor.Delete;
using Kollity.Application.Commands.Doctor.Edit;
using Kollity.Application.Dtos.Doctor;
using Kollity.Application.Queries.Doctor.GetById;
using Kollity.Application.Queries.Doctor.GetList;
using Kollity.Domain.Identity.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Kollity.API.Controllers;

public class DoctorController : BaseController
{
    [HttpPost]
    [Authorize(Roles = $"{Role.Admin}")]
    public Task<IResult> Add([FromBody] AddDoctorDto addDoctorDto)
    {
        return Send(new AddDoctorCommand(addDoctorDto));
    }

    [HttpGet("{id:guid}")]
    [SwaggerResponse(200, type: typeof(DoctorDto))]
    public Task<IResult> Get(Guid id)
    {
        return Send(new GetDoctorByIdQuery(id));
    }

    [HttpGet]
    [SwaggerResponse(200, type: typeof(List<DoctorForListDto>))]
    public Task<IResult> Get([FromQuery] DoctorListFilters filters)
    {
        return Send(new GetDoctorListQuery(filters));
    }

    [HttpPut]
    public Task<IResult> Edit([FromBody] EditDoctorDto editDoctorDto)
    {
        return Send(new EditDoctorCommand(editDoctorDto));
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = $"{Role.Admin}")]
    public Task<IResult> Delete(Guid id)
    {
        return Send(new DeleteDoctorCommand(id));
    }
}