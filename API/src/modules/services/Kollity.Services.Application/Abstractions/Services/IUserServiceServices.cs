using Kollity.Services.Domain.DoctorModels;
using Kollity.Services.Domain.StudentModels;

namespace Kollity.Services.Application.Abstractions.Services;

public interface IUserServiceServices
{
    Task<Result> AddStudent(Student student, string password);
    Task<Result> AddDoctor(Doctor doctor, string password, string role);
    Task<Result> EditUser(Guid id, string username);
    Task<Result> DeleteUser(Guid id);
}