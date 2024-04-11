using Kollity.Services.Application.Dtos.Course;
using Kollity.Services.Application.Dtos.Doctor;
using Kollity.Services.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Queries.Doctor.GetById;

public class GetDoctorByIdQueryHandler : IQueryHandler<GetDoctorByIdQuery, DoctorDto>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetDoctorByIdQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<DoctorDto>> Handle(GetDoctorByIdQuery request, CancellationToken cancellationToken)
    {
        var doctorDto = await _context.Doctors
            // .ProjectTo<DoctorDto>(_mapper.ConfigurationProvider)
            .Select(x => new DoctorDto
            {
                Id = x.Id,
                ProfileImage = x.ProfileImage,
                UserName = x.UserName,
                FullNameInArabic = x.FullNameInArabic,
                Courses = x.CoursesAssistants.Select(ca =>
                    new CourseForListDto
                    {
                        Id = ca.Course.Id,
                        Department = ca.Course.Department,
                        Name = ca.Course.Name,
                        Code = ca.Course.Code
                    }).ToList()
            })
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        var doctorCourses = await _context.Courses
            .Where(x => x.DoctorId == request.Id)
            .Select(c => new CourseForListDto
            {
                Id = c.Id,
                Department = c.Department,
                Name = c.Name,
                Code = c.Code
            })
            .ToListAsync(cancellationToken);

        doctorDto.Courses.AddRange(doctorCourses);

        return doctorDto is null ? DoctorErrors.IdNotFound(request.Id) : doctorDto;
    }
}