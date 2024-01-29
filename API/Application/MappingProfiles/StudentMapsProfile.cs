using Application.Dtos.Course;
using Application.Dtos.Student;
using Domain.StudentModels;

namespace Application.MappingProfiles;

public class StudentMapsProfile : Profile
{
    public StudentMapsProfile()
    {
        CreateMap<AddStudentDto, Student>();
        CreateMap<EditStudentDto, Student>();

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