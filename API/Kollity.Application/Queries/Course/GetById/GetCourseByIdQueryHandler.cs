using AutoMapper.QueryableExtensions;
using Kollity.Application.Abstractions;
using Kollity.Application.Dtos.Course;
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
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

        var idNameMaps = courseDto.Assistants.Select(x => new
        {
            x.Id,
            x.UserName
        }).ToDictionary(x => x.Id);
        if (courseDto.Doctor is not null)
            idNameMaps.Add(courseDto.Doctor.Id, new
            {
                courseDto.Doctor.Id,
                courseDto.Doctor.UserName
            });
        idNameMaps.Add(Guid.Empty, new
        {
            Id = Guid.Empty,
            UserName = "No Doctor"
        });

        courseDto.Rooms.ForEach(r => r.DoctorName = idNameMaps.GetValueOrDefault(r.DoctorId).UserName);

        return courseDto;
    }
}