using Application.Dtos.Course;
using Application.Dtos.Doctor;
using Domain.DoctorModels;

namespace Application.MappingProfiles;

public class DoctorMapsProfile : Profile
{
    public DoctorMapsProfile()
    {
        CreateMap<AddDoctorDto, Doctor>();
        CreateMap<Doctor, DoctorDto>()
            .ForMember(dest => dest.Courses, opt =>
                opt.MapFrom(src => src.Courses.Select(c => new CourseForListDto
                {
                    Id = c.Id,
                    Code = c.Code,
                    Department = c.Department,
                    Name = c.Name
                })));
        CreateMap<EditDoctorDto, Doctor>();
        CreateMap<Doctor, DoctorForListDto>();
    }
}