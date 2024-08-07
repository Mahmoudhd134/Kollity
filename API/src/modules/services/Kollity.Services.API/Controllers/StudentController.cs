﻿using Kollity.Services.Application.Commands.Student.Add;
using Kollity.Services.Application.Commands.Student.Delete;
using Kollity.Services.Application.Commands.Student.Edit;
using Kollity.Services.Application.Dtos.Course;
using Kollity.Services.Application.Dtos.Student;
using Kollity.Services.Application.Queries.Student.Courses;
using Kollity.Services.Application.Queries.Student.GetById;
using Kollity.Services.Application.Queries.Student.GetList;
using Kollity.Services.Application.Queries.Student.Profile;
using Kollity.Services.Domain.Identity;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Kollity.Services.API.Controllers;

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

    [HttpGet("{id:guid}/profile")]
    [SwaggerResponse(200, type: typeof(StudentProfileDto))]
    public Task<IResult> Profile(Guid id)
    {
        return Send(new GetStudentProfileByIdQuery(id));
    }

    [HttpGet("{id:guid}/courses")]
    [SwaggerResponse(200, type: typeof(List<CourseForListDto>))]
    public Task<IResult> Courses(Guid id)
    {
        return Send(new GetStudentCoursesByIdQuery(id));
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