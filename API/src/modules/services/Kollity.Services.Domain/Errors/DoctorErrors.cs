using Kollity.Common.ErrorHandling;

namespace Kollity.Services.Domain.Errors;

public static class DoctorErrors
{
    public static readonly Error UnAuthorizeEdit = Error.Validation("Doctor.UnAuthorizeEdit",
        "You can edit the details of another doctor");

    public static Error UserNameAlreadyExists(string userName)
    {
        return Error.Validation("Doctor.UserNameAlreadyExists",
            $"The user name '{userName}' already used by another user.");
    }

    public static Error IdNotFound(Guid id)
    {
        return Error.NotFound("Doctor.IdNotFound",
            $"There are no doctor with id '{id}'.");
    }
}