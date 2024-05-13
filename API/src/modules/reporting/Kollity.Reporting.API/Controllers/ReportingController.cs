﻿using Kollity.Reporting.Application.Dtos.Course;
using Kollity.Reporting.Application.Dtos.Room;
using Kollity.Reporting.Application.Queries.Course;
using Kollity.Reporting.Application.Queries.Room;
using Kollity.Reporting.Domain.UserModels;
using Kollity.Reporting.Persistence.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Kollity.Reporting.API.Controllers;

[Route("api/report")]
public class ReportingController(ReportingDbContext context) : BaseController
{
    [HttpGet("course/{courseId:guid}"),
     Authorize(Roles = $"{Role.Admin},{Role.Doctor},{Role.Assistant}"),
     SwaggerResponse(200, type: typeof(CourseReportDto))]
    public Task<IResult> CourseReport(Guid courseId, DateTime? from = null, DateTime? to = null)
    {
        return Send(new GetCourseReportQuery(courseId, from, to));
    }

    [HttpGet("room/{roomId:guid}"),
     Authorize(Roles = $"{Role.Admin},{Role.Doctor},{Role.Assistant}"),
     SwaggerResponse(200, type: typeof(RoomReportDto))]
    public Task<IResult> RoomReport(Guid roomId)
    {
        return Send(new RoomReportQuery(roomId));
    }
}