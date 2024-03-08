using AutoMapper.QueryableExtensions;
using Kollity.Application.Abstractions;
<<<<<<< HEAD
=======
using Kollity.Application.Abstractions.Services;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
using Kollity.Application.Dtos.Course;
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Queries.Course.GetById;

public class GetCourseByIdQueryHandler : IQueryHandler<GetCourseByIdQuery, CourseDto>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
<<<<<<< HEAD
    private readonly IUserAccessor _userAccessor;

    public GetCourseByIdQueryHandler(ApplicationDbContext context, IMapper mapper, IUserAccessor userAccessor)
    {
        _context = context;
        _mapper = mapper;
        _userAccessor = userAccessor;
=======
    private readonly IUserServices _userServices;

    public GetCourseByIdQueryHandler(ApplicationDbContext context, IMapper mapper, IUserServices userServices)
    {
        _context = context;
        _mapper = mapper;
        _userServices = userServices;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
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

<<<<<<< HEAD
        var currentUserId = _userAccessor.GetCurrentUserId();
=======
        var currentUserId = _userServices.GetCurrentUserId();
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
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