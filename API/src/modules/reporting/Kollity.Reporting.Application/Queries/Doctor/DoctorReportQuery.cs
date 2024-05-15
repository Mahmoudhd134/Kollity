using Kollity.Reporting.Application.Dtos.Doctor;

namespace Kollity.Reporting.Application.Queries.Doctor;

public record DoctorReportQuery(Guid Id, DateTime? From, DateTime? To) : IQuery<DoctorReportDto>;