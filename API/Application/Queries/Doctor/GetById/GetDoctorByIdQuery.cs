using Application.Dtos.Doctor;

namespace Application.Queries.Doctor.GetById;

public record GetDoctorByIdQuery(Guid Id) : IQuery<DoctorDto>;