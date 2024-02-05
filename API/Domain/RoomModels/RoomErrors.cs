using Domain.ErrorHandlers;

namespace Domain.RoomModels;

public static class RoomErrors
{
    public static Error IdNotFound(Guid roomId) => Error.NotFound("Room.WrongId",
        $"The id '{roomId}' is wrong.");

    public static readonly Error UnAuthorizeEdit = Error.Validation("Room.UnAuthorizeEdit",
        "You can edit the details of a room you don't own.");

    public static readonly Error UnAuthorizeDelete = Error.Validation("Room.UnAuthorizeDelete",
        "You do not have the right permissions to delete this room.");

    public static readonly Error UnAuthorizeAcceptJoinRequest = Error.Validation("Room.UnAuthorizeAcceptJoinRequest",
        "You do not have the right permissions to accept join requests in this room.");

    public static readonly Error UnAuthorizeDenyJoinRequest = Error.Validation("Room.UnAuthorizeDenyJoinRequest",
        "You do not have the right permissions to deny join requests in this room.");

    public static readonly Error UserAlreadyJoinedTheRoom = Error.Conflict("Room.UserAlreadyJoinedTheRoom",
        "The user is already a member in this room.");
}