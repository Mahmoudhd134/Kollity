using Kollity.Common.ErrorHandling;
using Kollity.Services.Application.Abstractions.Services;
using Kollity.Services.Domain.DoctorModels;
using Kollity.Services.Domain.Identity;
using Kollity.Services.Domain.StudentModels;
using Kollity.User.Contracts;

namespace Kollity.Services.Infrastructure.Implementation.IntegrationServices;

public class UserServiceServices(IUserIntegrationServices userIntegrationServices) : IUserServiceServices
{
    public Task<Result> AddStudent(Student student, string password)
    {
        return userIntegrationServices.AddUser(
            student.UserName,
            student.Email,
            password,
            UserRole.Student
        );
    }

    public Task<Result> AddDoctor(Doctor doctor, string password, string role)
    {
        return userIntegrationServices.AddUser(
            doctor.UserName,
            doctor.Email,
            password,
            role == Role.Doctor ? UserRole.Doctor : UserRole.Assistant
        );
    }
}