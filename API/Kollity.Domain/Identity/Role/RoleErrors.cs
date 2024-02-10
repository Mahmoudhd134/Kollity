using Kollity.Domain.ErrorHandlers;

namespace Kollity.Domain.Identity.Role;

public static class RoleErrors
{
    public static Error RoleNotFound(string role)
    {
        return Error.NotFound("Role.NotFound",
            $"The role with the name '{role}' is not found");
    }
}