using Kollity.Reporting.Application.Dtos.Room;

namespace Kollity.Reporting.Application.Queries.Room;

public record RoomReportQuery(Guid Id) : IQuery<RoomReportDto>;