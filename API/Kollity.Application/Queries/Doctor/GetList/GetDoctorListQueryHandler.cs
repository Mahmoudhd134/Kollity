using AutoMapper.QueryableExtensions;
using Kollity.Application.Dtos.Doctor;
using Kollity.Domain.Identity.Role;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Queries.Doctor.GetList;

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

        if (string.IsNullOrWhiteSpace(filters.Role) == false)
        {
            var role = request.Filters.Role.ToUpper();
            var roleId = await _context.Roles
                .Where(x => x.NormalizedName == role)
                .Select(x => x.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (roleId == Guid.Empty)
                return RoleErrors.RoleNotFound(request.Filters.Role);
            doctors = doctors.Where(x => x.Roles.Any(xx => xx.RoleId == roleId));
        }

        return await doctors
            .Skip(filters.PageIndex * filters.PageSize)
            .Take(filters.PageSize)
            .OrderBy(x => x.UserName)
            .ProjectTo<DoctorForListDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}