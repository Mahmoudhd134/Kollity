using Domain.ErrorHandlers;

namespace Domain.DoctorModels;

public static class DoctorErrors
{
    public static Error UserNameAlreadyExists(string userName) => Error.Validation("Doctor.UserNameAlreadyExists",
        $"The user name '{userName}' already used by another user.");
    
    public static readonly Error UnAuthorizeEdit = Error.Validation("Doctor.UnAuthorizeEdit",
        "You can edit the details of another doctor");

    public static Error IdNotFound(Guid id) => Error.NotFound("Doctor.IdNotFound",
        $"There are no doctor with id '{id}'.");
}