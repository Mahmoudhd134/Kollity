using AutoMapper.QueryableExtensions;
using Kollity.Application.Abstractions;
using Kollity.Application.Dtos.Course;
using Kollity.Domain.CourseModels;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Queries.Course.GetById;

public class GetCourseByIdQueryHandler : IQueryHandler<GetCourseByIdQuery, CourseDto>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUserAccessor _userAccessor;

    public GetCourseByIdQueryHandler(ApplicationDbContext context, IMapper mapper, IUserAccessor userAccessor)
    {
        _context = context;
        _mapper = mapper;
        _userAccessor = userAccessor;
    }

    public async Task<Result<CourseDto>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
    {
        //todo -> make a benchmark comparing split query vs single query performance in this case
        var courseDto = await _context.Courses
            .AsSplitQuery()
            .ProjectTo<CourseDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (courseDto is null)
            return CourseErrors.IdNotFound(request.Id);

        var currentUserId = _userAccessor.GetCurrentUserId();
        courseDto.IsTheDoctorAssigned = courseDto.Doctor?.Id == currentUserId ||
                                        courseDto.Assistants.Any(x => x.Id == currentUserId);

        return courseDto;
    }
}