using Domain.ErrorHandlers;

namespace Domain.Identity.User;

public static class UserErrors
{
    public static readonly Error WeakPassword = Error.Validation("User.WeakPassword",
        "The password must has more than 8 characters and at least one small and one capital character and one non-alphabitic character and at least one number");

    public static readonly Error WrongUsername = Error.NotFound("User.WrongUsername",
        "The username is wrong");

    public static readonly Error WrongPassword = Error.Validation("User.WrongPassword",
        "The password is wrong");

    public static readonly Error ExpireRefreshToken = Error.Validation("User.ExpireRefreshToken",
        "The session is expired, signin again");

    public static Error EmailNotFound(string email) => Error.NotFound("User.EmailNotFound",
        $"There is no user with the email '{email}'.");

    public static Error IdNotFound(Guid id) => Error.NotFound("User.IdNotFound",
        $"There is no user with the id '{id}'.");

    public static readonly Error WrongRefreshToken = Error.NotFound("User.WrongRefreshToken",
        "The Refresh Token Is Wrong, Sign In Again");

    public static readonly Error UnAuthorizedEdit = Error.Validation("User.UnAuthorizedEdit",
        "You can not edit an user that is not you");

    public static readonly Error NotSignedIn = Error.Validation("User.NotSignedIn",
        "You are not signed in, please sign in.");

    public static Error UserNameAlreadyUsed(string userName) =>
        Error.Conflict("User.UserNameAlreadyUsed", $"User name '{userName}' is already used");

    public static Error EmailAlreadyUsed(string email) =>
        Error.Conflict("User.EmailAlreadyUsed", $"The email '{email}' is already exists");
}