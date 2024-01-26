using Application.Dtos.Student;
using AutoMapper;

namespace Application.MappingProfiles;

public class StudentMapsProfile : Profile
{
    public StudentMapsProfile()
    {
        CreateMap<AddStudentDto, Domain.StudentModels.Student>();
    }
}