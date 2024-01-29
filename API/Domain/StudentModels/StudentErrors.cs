using Domain.ErrorHandlers;

namespace Domain.StudentModels;

public static class StudentErrors
{
    public static Error CodeAlreadyExists(string code) =>
        Error.Conflict("Student.CodeAlreadyExists", $"The code '{code} is already exists.");

    public static readonly Error UnAuthorizeEdit = Error.Validation("Student.UnAuthorizeEdit",
        "You can edit the details of another student");

    public static Error IdNotFound(Guid id) => Error.NotFound("Student.IdNotFound",
        $"There are no student with id '{id}'.");
}