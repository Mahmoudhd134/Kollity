using Kollity.Reporting.Application.Exceptions.Generic;

namespace Kollity.Reporting.Application.Exceptions;

public static class AssignmentExceptions
{
    public class AssignmentNotFound(Guid id) : NotFoundException($"Assignment with id {id} not found");

    public class AnswerNotFound(Guid assignmentId, List<Guid> studentIds) : NotFoundException(
        $"Assignment answer not found for assignment id {assignmentId} and students id {string.Join(", ", studentIds)}");
    
    public class GroupNotFound(Guid id) : NotFoundException($"Assignment group with id {id} not found");
}