using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Dtos.Doctor;

namespace Kollity.Services.Application.Queries.Doctor.GetList;

public record GetDoctorListQuery(DoctorListFilters Filters) : IQuery<List<DoctorForListDto>>;