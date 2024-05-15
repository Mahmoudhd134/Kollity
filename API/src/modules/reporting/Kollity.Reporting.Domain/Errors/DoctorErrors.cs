using Kollity.Common.ErrorHandling;

namespace Kollity.Reporting.Domain.Errors;

public static class DoctorErrors
{
    public static Error IdNotFound(Guid id)
    {
        return Error.NotFound("Doctor.IdNotFound",
            $"There are no doctor with id '{id}'.");
    }
}