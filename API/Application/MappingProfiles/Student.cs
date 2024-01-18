using Application.Commands.Student.AddStudent;
using Application.Dtos.Student;
using AutoMapper;

namespace Application.MappingProfiles;

public class Student : Profile
{
    public Student()
    {
        CreateMap<AddStudentDto, Domain.StudentModels.Student>();
    }
}