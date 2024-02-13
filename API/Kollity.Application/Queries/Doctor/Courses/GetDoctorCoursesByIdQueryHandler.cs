using AutoMapper.QueryableExtensions;
using Kollity.Application.Abstractions;
using Kollity.Application.Dtos.Course;
using Kollity.Domain.Identity.Role;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Queries.Doctor.Courses;

public class GetDoctorCoursesByIdQueryHandler : IQueryHandler<GetDoctorCoursesByIdQuery, List<CourseForListDto>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUserAccessor _userAccessor;

    public GetDoctorCoursesByIdQueryHandler(ApplicationDbContext context, IMapper mapper, IUserAccessor userAccessor)
    {
        _context = context;
        _mapper = mapper;
        _userAccessor = userAccessor;
    }

    public async Task<Result<List<CourseForListDto>>> Handle(GetDoctorCoursesByIdQuery request,
        CancellationToken cancellationToken)
    {
        var roles = _userAccessor.GetCurrentUserRoles();
        var isDoctor = roles.Any(x => x == Role.Doctor);
        var isAssistant = roles.Any(x => x == Role.Assistant);

        List<CourseForListDto> courses = [];
        
        if (isDoctor)
            courses.AddRange(
                await _context.Courses
                    .Where(x => x.DoctorId == request.Id)
                    .ProjectTo<CourseForListDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken)
            );
        
        if (isAssistant)
            courses.AddRange(
                await _context.CourseAssistants
                    .Where(x => x.AssistantId == request.Id)
                    .Select(x => x.Course)
                    .ProjectTo<CourseForListDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken)
            );
        return courses;
    }
}