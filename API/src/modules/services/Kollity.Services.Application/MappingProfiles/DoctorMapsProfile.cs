using Kollity.Services.Domain.DoctorModels;
using Kollity.Services.Application.Dtos.Doctor;

namespace Kollity.Services.Application.MappingProfiles;

public class DoctorMapsProfile : Profile
{
    public DoctorMapsProfile()
    {
        CreateMap<AddDoctorDto, Doctor>();
        CreateMap<Doctor, DoctorProfileDto>();
        // CreateMap<Doctor, DoctorDto>()
        //     .ForMember(dest => dest.Courses, opt =>
        //         opt.MapFrom(src => src.Courses.Select(c => new CourseForListDto
        //         {
        //             Id = c.Id,
        //             Code = c.Code,
        //             Department = c.Department,
        //             Name = c.Name
        //         }).Concat(src.CoursesAssistants.Select(ca => new CourseForListDto
        //         {
        //             Id = ca.Course.Id,
        //             Code = ca.Course.Code,
        //             Department = ca.Course.Department,
        //             Name = ca.Course.Name
        //         })).ToList()));
        CreateMap<EditDoctorDto, Doctor>();
        CreateMap<Doctor, DoctorForListDto>();
    }
}