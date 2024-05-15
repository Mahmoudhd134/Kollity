using Kollity.Common.ErrorHandling;

namespace Kollity.Services.Domain.Errors;

public static class RoleErrors
{
    public static Error RoleNotFound(string role)
    {
        return Error.NotFound("Role.NotFound",
            $"The role with the name '{role}' is not found");
    }
}