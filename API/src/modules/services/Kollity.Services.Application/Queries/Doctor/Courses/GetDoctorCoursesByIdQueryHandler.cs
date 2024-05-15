using AutoMapper.QueryableExtensions;
using Kollity.Services.Application.Dtos.Course;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Queries.Doctor.Courses;

public class GetDoctorCoursesByIdQueryHandler : IQueryHandler<GetDoctorCoursesByIdQuery, List<CourseForListDto>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetDoctorCoursesByIdQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<List<CourseForListDto>>> Handle(GetDoctorCoursesByIdQuery request,
        CancellationToken cancellationToken)
    {
        List<CourseForListDto> courses = [];

        courses.AddRange(
            await _context.Courses
                .Where(x => x.DoctorId == request.Id)
                .ProjectTo<CourseForListDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
        );

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