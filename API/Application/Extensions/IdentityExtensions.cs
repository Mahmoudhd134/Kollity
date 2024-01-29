using Microsoft.AspNetCore.Identity;

namespace Application.Extensions;

public static class IdentityExtensions
{
    public static IEnumerable<Error> ToAppError(this IEnumerable<IdentityError> errors)
    {
        return errors.Select(x => Error.Validation(x.Code, x.Description));
    }
}