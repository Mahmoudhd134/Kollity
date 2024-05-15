using Kollity.Services.Application.Dtos.Doctor;

namespace Kollity.Services.Application.Queries.Doctor.GetById;

public record GetDoctorByIdQuery(Guid Id) : IQuery<DoctorDto>;