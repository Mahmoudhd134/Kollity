using Kollity.Reporting.Application.Exceptions.Generic;

namespace Kollity.Reporting.Application.Exceptions;

public static class RoomExceptions
{
    public class RoomNotFound(Guid id) : NotFoundException($"Room with id {id} not found");

    public class RoomContentNotFound(Guid id) : NotFoundException($"Room content with id {id} not found");
}