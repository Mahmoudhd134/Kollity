using Kollity.Application.Dtos.Doctor;

namespace Kollity.Application.Queries.Doctor.GetList;

public record GetDoctorListQuery(DoctorListFilters Filters) : IQuery<List<DoctorForListDto>>;