using Kollity.Application.Dtos.Doctor;

namespace Kollity.Application.Queries.Doctor.Profile;

public record GetDoctorProfileByIdQuery(Guid Id) : IQuery<DoctorProfileDto>;