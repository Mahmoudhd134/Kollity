using Domain.ErrorHandlers;
using Domain.StudentModels;
using Microsoft.AspNetCore.Identity;

namespace Persistence.Implementation;

public class EfCoreStudentRepo : IStudentRepository
{
    private readonly UserManager<Student> _studentManager;

    public EfCoreStudentRepo(UserManager<Student> studentManager)
    {
        _studentManager = studentManager;
    }

    public async Task<List<Error>> CreateAsync(Student student)
    {
        var result = await _studentManager.CreateAsync(student);
        return result.Errors
            .Select(e => new Error(e.Code, e.Description))
            .ToList();
    }
}