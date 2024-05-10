using Kollity.Reporting.Application.Dtos.Course;
using Kollity.Reporting.Application.Queries.Course;
using Kollity.Reporting.Domain.UserModels;
using Kollity.Reporting.Persistence.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Kollity.Reporting.API.Controllers;

[Microsoft.AspNetCore.Components.Route("api/report")]
public class ReportingController(ReportingDbContext context) : BaseController
{
    [HttpGet("course/{courseId:guid}"),
     Authorize(Roles = $"{Role.Admin},{Role.Doctor},{Role.Assistant}"),
     SwaggerResponse(200, type: typeof(CourseReportDto))]
    public Task<IResult> Get(Guid courseId)
    {
        return Send(new GetCourseReportQuery(courseId));
    }
}