using Kollity.Reporting.Application.Dtos.Course;
using Kollity.Reporting.Application.Dtos.Doctor;
using Kollity.Reporting.Application.Dtos.Exam;
using Kollity.Reporting.Application.Dtos.Room;
using Kollity.Reporting.Application.Dtos.Student;
using Kollity.Reporting.Application.Queries.Course;
using Kollity.Reporting.Application.Queries.Doctor;
using Kollity.Reporting.Application.Queries.Exam;
using Kollity.Reporting.Application.Queries.Room;
using Kollity.Reporting.Application.Queries.Student;
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
     // Authorize(Roles = $"{Role.Admin},{Role.Doctor},{Role.Assistant}"),
     AllowAnonymous,
     SwaggerResponse(200, type: typeof(RoomReportDto))]
    public Task<IResult> RoomReport(Guid roomId)
    {
        return Send(new RoomReportQuery(roomId));
    }

    [HttpGet("doctor/{doctorId:guid}"),
     Authorize(Roles = $"{Role.Admin},{Role.Doctor},{Role.Assistant}"),
     SwaggerResponse(200, type: typeof(DoctorReportDto))]
    public Task<IResult> DoctorReport(Guid doctorId, DateTime? from = null, DateTime? to = null)
    {
        return Send(new DoctorReportQuery(doctorId, from, to));
    }

    [HttpGet("student/{studentId:guid}"),
     Authorize(Roles = $"{Role.Admin},{Role.Doctor},{Role.Assistant}"),
     SwaggerResponse(200, type: typeof(StudentReportDto))]
    public Task<IResult> StudentReport(Guid studentId, DateTime? from = null, DateTime? to = null)
    {
        return Send(new StudentReportQuery(studentId, from, to));
    }

    [HttpGet("exam/{examId:guid}"),
     // Authorize(Roles = $"{Role.Admin},{Role.Doctor},{Role.Assistant}"),
     AllowAnonymous,
     SwaggerResponse(200, type: typeof(ExamStatisticsDto))]
    public Task<IResult> ExamStatistics(Guid examId)
    {
        return Send(new GetExamStatisticsQuery(examId));
    }
}