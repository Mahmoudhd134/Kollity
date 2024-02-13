using Kollity.Application.Dtos.Course;
using Kollity.Application.Dtos.Student;
using Kollity.Domain.StudentModels;

namespace Kollity.Application.MappingProfiles;

public class StudentMapsProfile : Profile
{
    public StudentMapsProfile()
    {
        CreateMap<AddStudentDto, Student>();
        CreateMap<EditStudentDto, Student>();

        CreateMap<Student, StudentProfileDto>();
        CreateMap<Student, StudentDto>()
            .ForMember(dest => dest.Courses, opt =>
                opt.MapFrom(src => src.StudentsCourses.Select(sc => new CourseForListDto
                {
                    Id = sc.CourseId,
                    Code = sc.Course.Code,
                    Department = sc.Course.Department,
                    Name = sc.Course.Name
                })));

        CreateMap<Student, StudentForListDto>();
    }
}