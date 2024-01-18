using Domain.ErrorHandlers;

namespace Domain.StudentModels;

public interface IStudentRepository
{
    Task<List<Error>> CreateAsync(Student student);
}