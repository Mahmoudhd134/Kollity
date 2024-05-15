using Kollity.Services.Application.Dtos.Doctor;

namespace Kollity.Services.Application.Queries.Doctor.Profile;

public record GetDoctorProfileByIdQuery(Guid Id) : IQuery<DoctorProfileDto>;