using Kollity.Services.Application.Commands.Doctor.Add;
using Kollity.Services.Application.Commands.Doctor.Delete;
using Kollity.Services.Application.Commands.Doctor.Edit;
using Kollity.Services.Application.Dtos.Course;
using Kollity.Services.Application.Dtos.Doctor;
using Kollity.Services.Application.Queries.Doctor.Courses;
using Kollity.Services.Application.Queries.Doctor.GetById;
using Kollity.Services.Application.Queries.Doctor.GetList;
using Kollity.Services.Application.Queries.Doctor.Profile;
using Kollity.Services.Domain.Identity.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Kollity.Services.API.Controllers;

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

    [HttpGet("{id:guid}/profile")]
    [SwaggerResponse(200, type: typeof(DoctorProfileDto))]
    public Task<IResult> Profile(Guid id)
    {
        return Send(new GetDoctorProfileByIdQuery(id));
    }

    [HttpGet("{id:guid}/courses")]
    [SwaggerResponse(200, type: typeof(List<CourseForListDto>))]
    public Task<IResult> Courses(Guid id)
    {
        return Send(new GetDoctorCoursesByIdQuery(id));
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