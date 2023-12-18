using Domain.ErrorHandlers;

namespace Domain.Identity;

public static class UserErrors
{
    public static readonly Error WeakPassword = new("User.WeakPassword",
        "The password must has more than 8 characters and at least one small and one capital character and one non-alphabitic character and at least one number");

    public static readonly Error WrongUsername = new("User.WrongUsername",
        "The username is wrong");

    public static readonly Error WrongPassword = new("User.WrongPassword",
        "The password ss wrong");

    public static readonly Error ExpireRefreshToken = new("User.ExpireRefreshToken",
        "The session is expired, signin again");

    public static readonly Error WrongId = new("User.WrongId",
        "The Id is wrong");

    public static readonly Error WrongRefreshToken = new("User.WrongRefreshToken",
        "The Refresh Token Is Wrong, Sign In Again");

    public static readonly Error UnAuthorizedEdit = new("User.UnAuthorizedEdit",
        "You can not edit an user that is not you");

    public static readonly Error NotSignedIn = new("User.NotSignedIn",
        "You are not signed in, please sign in.");

    public static Error UserNameAlreadyUsed(string userName)
    {
        return new("User.UserNameAlreadyUsed",
            $"User name '{userName}' is already used");
    }

    public static Error EmailAlreadyUsed(string email)
    {
        return new("User.EmailAlreadyUsed",
            $"The email '{email}' is already exists");
    }
}