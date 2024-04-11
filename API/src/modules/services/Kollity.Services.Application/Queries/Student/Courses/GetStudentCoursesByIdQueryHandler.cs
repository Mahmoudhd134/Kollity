using AutoMapper.QueryableExtensions;
using Kollity.Services.Application.Dtos.Course;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Queries.Student.Courses;

public class GetStudentCoursesByIdQueryHandler : IQueryHandler<GetStudentCoursesByIdQuery, List<CourseForListDto>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetStudentCoursesByIdQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<List<CourseForListDto>>> Handle(GetStudentCoursesByIdQuery request,
        CancellationToken cancellationToken)
    {
        var courses = await _context.StudentCourses
            .Where(x => x.StudentId == request.Id)
            .Select(x => x.Course)
            .ProjectTo<CourseForListDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return courses;
    }
}