using Kollity.Domain.ErrorHandlers;

namespace Kollity.Domain.StudentModels;

public static class StudentErrors
{
    public static readonly Error UnAuthorizeEdit = Error.Validation("Student.UnAuthorizeEdit",
        "You can edit the details of another student");

    public static Error CodeAlreadyExists(string code)
    {
        return Error.Conflict("Student.CodeAlreadyExists", $"The code '{code} is already exists.");
    }

    public static Error IdNotFound(Guid id)
    {
        return Error.NotFound("Student.IdNotFound",
            $"There are no student with id '{id}'.");
    }
}