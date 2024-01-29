using Application.Dtos.Course;
using AutoMapper.QueryableExtensions;
using Domain.CourseModels;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Course.GetById;

public class GetCourseByIdQueryHandler : IQueryHandler<GetCourseByIdQuery, CourseDto>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCourseByIdQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<CourseDto>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
    {
        //todo -> make a benchmark comparing split query vs single query performance in this case
        var courseDto = await _context.Courses
            .AsSplitQuery()
            .ProjectTo<CourseDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        return courseDto is not null ? courseDto : CourseErrors.WrongId(request.Id);
    }
}