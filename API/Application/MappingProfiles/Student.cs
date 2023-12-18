using Application.Commands.Student.AddStudent;
using AutoMapper;

namespace Application.MappingProfiles;

public class Student : Profile
{
    public Student()
    {
        CreateMap<AddStudentDto, Domain.Student.Student>();
    }
}