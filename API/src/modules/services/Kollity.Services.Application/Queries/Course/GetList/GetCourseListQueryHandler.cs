using AutoMapper.QueryableExtensions;
using Kollity.Services.Domain.ErrorHandlers.Abstractions;
using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Dtos.Course;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Queries.Course.GetList;

public class GetCourseListQueryHandler : IQueryHandler<GetCourseListQuery, List<CourseForListDto>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCourseListQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<List<CourseForListDto>>> Handle(GetCourseListQuery request,
        CancellationToken cancellationToken)
    {
        var filters = request.CourseListFilters;
        var courses = _context.Courses.AsQueryable();

        if (string.IsNullOrWhiteSpace(filters.Department) == false)
            courses = courses
                .Where(x => x.Department.ToUpper() == filters.Department.ToUpper());

        if (string.IsNullOrWhiteSpace(filters.NamePrefix) == false)
            courses = courses
                .Where(x => x.Name.ToUpper().StartsWith(filters.NamePrefix.ToUpper()));

        if (filters.Code is not null)
        {
            var codeAsString = filters.Code.ToString();
            courses = courses
                .Where(x => x.Code.ToString().StartsWith(codeAsString));
        }

        if (filters.HasADoctor != null)
            if (filters.HasADoctor ?? false)
                courses = courses
                    .Where(x => x.DoctorId != null);
            else
                courses = courses
                    .Where(x => x.DoctorId == null);

        var courseDtos = courses.ProjectTo<CourseForListDto>(_mapper.ConfigurationProvider);

        return await courseDtos
            .OrderBy(c => c.Name)
            .Skip(filters.PageIndex * filters.PageSize)
            .Take(filters.PageSize)
            .ToListAsync(cancellationToken);
    }
}