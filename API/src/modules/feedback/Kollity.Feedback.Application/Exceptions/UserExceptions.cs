using Kollity.Feedback.Application.Exceptions.Generic;

namespace Kollity.Feedback.Application.Exceptions;

public static class UserExceptions
{
    public class DoctorNotFound(Guid id) : NotFoundException($"Doctor with id {id} not found");

    public class StudentNotFound(Guid id) : NotFoundException($"Student with id {id} not found");
    public class UserNotFound(Guid id) : NotFoundException($"User with id {id} not found");
}