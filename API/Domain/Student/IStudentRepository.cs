using System.Collections;
using Domain.ErrorHandlers;

namespace Domain.Student;

public interface IStudentRepository
{
    Task<List<Error>> CreateAsync(Student student);
}