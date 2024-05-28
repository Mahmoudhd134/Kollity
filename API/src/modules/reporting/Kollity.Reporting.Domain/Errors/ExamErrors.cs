using Kollity.Common.ErrorHandling;

namespace Kollity.Reporting.Domain.Errors;

public static class ExamErrors
{
    public static Error NotFound(Guid id)
    {
        return Error.NotFound("Exam.NotFound", $"The id '{id}' is wrong.");
    }
}