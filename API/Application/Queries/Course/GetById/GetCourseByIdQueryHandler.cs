using Application.Dtos.Course;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.CourseModels;
using Domain.ErrorHandlers;
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
        var courseDto = await _context.Courses
            .ProjectTo<CourseDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        return courseDto is not null ? courseDto : CourseErrors.WrongId(request.Id);
    }
}