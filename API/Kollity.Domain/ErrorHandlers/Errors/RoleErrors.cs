using Kollity.Domain.ErrorHandlers.Abstractions;

namespace Kollity.Domain.ErrorHandlers.Errors;

public static class RoleErrors
{
    public static Error RoleNotFound(string role)
    {
        return Error.NotFound("Role.NotFound",
            $"The role with the name '{role}' is not found");
    }
}