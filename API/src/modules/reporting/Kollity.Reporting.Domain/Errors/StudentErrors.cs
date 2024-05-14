using Kollity.Common.ErrorHandling;

namespace Kollity.Reporting.Domain.Errors;

public static class StudentErrors
{
    public static Error IdNotFound(Guid id)
    {
        return Error.NotFound("Student.IdNotFound",
            $"There are no student with id '{id}'.");
    }
}