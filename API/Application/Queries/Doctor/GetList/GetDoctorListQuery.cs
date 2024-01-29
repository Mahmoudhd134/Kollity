using Application.Dtos.Doctor;

namespace Application.Queries.Doctor.GetList;

public record GetDoctorListQuery(DoctorListFilters Filters) : IQuery<List<DoctorForListDto>>;