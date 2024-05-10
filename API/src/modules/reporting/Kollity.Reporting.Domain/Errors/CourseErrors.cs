using Kollity.Common.ErrorHandling;

namespace Kollity.Reporting.Domain.Errors;

public static class CourseErrors
{
    public static Error IdNotFound(Guid courseId)
    {
        return Error.NotFound("Course.WrongId",
            $"The id '{courseId}' is wrong.");
    }
}