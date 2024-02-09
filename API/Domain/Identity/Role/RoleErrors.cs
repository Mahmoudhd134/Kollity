using Domain.ErrorHandlers;

namespace Domain.Identity.Role;

public static class RoleErrors
{
    public static Error RoleNotFound(string role) => Error.NotFound("Role.NotFound",
        $"The role with the name '{role}' is not found");
}