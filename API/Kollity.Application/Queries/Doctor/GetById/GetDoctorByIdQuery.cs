using Kollity.Application.Dtos.Doctor;

namespace Kollity.Application.Queries.Doctor.GetById;

public record GetDoctorByIdQuery(Guid Id) : IQuery<DoctorDto>;