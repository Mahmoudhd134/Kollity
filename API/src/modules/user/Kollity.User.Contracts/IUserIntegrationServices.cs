using Kollity.Common.ErrorHandling;

namespace Kollity.User.Contracts;

public interface IUserIntegrationServices
{
    Task<Result> AddUser(Guid id, string username, string email, string password, UserRole userRole);
}