using Kollity.Services.Domain.ErrorHandlers.Abstractions;
using Kollity.Services.Domain.ErrorHandlers.Errors;
using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Dtos.Course;
using Kollity.Services.Application.Dtos.Doctor;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Queries.Doctor.GetById;

public class GetDoctorByIdQueryHandler : IQueryHandler<GetDoctorByIdQuery, DoctorDto>
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<Domain.DoctorModels.Doctor> _doctorManager;
    private readonly IMapper _mapper;

    public GetDoctorByIdQueryHandler(ApplicationDbContext context,
        IMapper mapper,
        UserManager<Domain.DoctorModels.Doctor> doctorManager)
    {
        _context = context;
        _mapper = mapper;
        _doctorManager = doctorManager;
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