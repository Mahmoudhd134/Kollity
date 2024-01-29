using Application.Dtos.Doctor;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Doctor.GetList;

public class GetDoctorListQueryHandler : IQueryHandler<GetDoctorListQuery, List<DoctorForListDto>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetDoctorListQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public async Task<Result<List<DoctorForListDto>>> Handle(GetDoctorListQuery request,
        CancellationToken cancellationToken)
    {
        var filters = request.Filters;
        var doctors = _context.Doctors.AsQueryable();

        if (string.IsNullOrWhiteSpace(filters.UserNamePrefix) == false)
            doctors = doctors.Where(x => x.NormalizedUserName.StartsWith(filters.UserNamePrefix.ToUpper()));

        return await doctors
            .Skip(filters.PageIndex * filters.PageSize)
            .Take(filters.PageSize)
            .OrderBy(x => x.UserName)
            .ProjectTo<DoctorForListDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}