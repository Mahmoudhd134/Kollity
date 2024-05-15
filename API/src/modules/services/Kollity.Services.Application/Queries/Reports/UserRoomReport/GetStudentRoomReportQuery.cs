using Kollity.Services.Application.Dtos.Reports;

namespace Kollity.Services.Application.Queries.Reports.UserRoomReport;

public record GetStudentRoomReportQuery(Guid StudentId, Guid RoomId) : IQuery<StudentRoomReportDto>;