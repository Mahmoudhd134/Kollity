using Application.Dtos.Student;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Student.GetList;

public class GetStudentListQueryHandler : IQueryHandler<GetStudentListQuery, List<StudentForListDto>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetStudentListQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<List<StudentForListDto>>> Handle(GetStudentListQuery request,
        CancellationToken cancellationToken)
    {
        var filters = request.Filters;
        var students = _context.Students.AsQueryable();

        if (string.IsNullOrWhiteSpace(filters.Code) == false)
            students = students.Where(x => x.Code.StartsWith(filters.Code));

        if (string.IsNullOrWhiteSpace(filters.UserNamePrefix) == false)
            students = students.Where(x => x.NormalizedUserName.StartsWith(filters.UserNamePrefix.ToUpper()));

        return await students
            .Skip(filters.PageIndex * filters.PageSize)
            .Take(filters.PageSize)
            .OrderBy(x => x.UserName)
            .ProjectTo<StudentForListDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}