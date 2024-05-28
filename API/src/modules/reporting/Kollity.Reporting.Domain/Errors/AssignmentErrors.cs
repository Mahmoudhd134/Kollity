using Kollity.Common.ErrorHandling;

namespace Kollity.Reporting.Domain.Errors;

public static class AssignmentErrors
{
    public static Error NotFound(Guid id)
    {
        return Error.NotFound("Assignment.NotFound", $"The id '{id}' is wrong.");
    }
}