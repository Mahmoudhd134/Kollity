using Kollity.Common.ErrorHandling;

namespace Kollity.Reporting.Domain.Errors;

public static class RoomErrors
{
    public static Error NotFound(Guid roomId)
    {
        return Error.NotFound("Room.NotFound", $"The id '{roomId}' is wrong.");
    }

    public static Error NotFound(string roomName)
    {
        return Error.NotFound("Room.NotFound", $"The name '{roomName}' is wrong.");
    }
}