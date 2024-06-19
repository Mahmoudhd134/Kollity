using Kollity.Feedback.Application.Exceptions.Generic;

namespace Kollity.Feedback.Application.Exceptions;

public static class RoomExceptions
{
    public class RoomNotFound(Guid id) : NotFoundException($"Room with id {id} not found");
}