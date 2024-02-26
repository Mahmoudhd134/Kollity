using System.Globalization;
using Kollity.Domain.ErrorHandlers.Abstractions;

namespace Kollity.Domain.ErrorHandlers.Errors;

public static class AssignmentErrors
{
    public static readonly Error UserIsNotInTheGroup = Error.Validation("Group.UserIsNotIn",
        "The user is not in this group.");

    public static readonly Error StudentIsInAnotherGroup = Error.Validation("Group.StudentIsInAnotherGroup",
        "The student can not join more than one group within the same room in the same time");

    public static readonly Error StudentIsWaitingOnThisGroup = Error.Conflict("Assignment.StudentIsWaitingOnThisGroup",
        "The student is already waiting to join this group");

    public static readonly Error UnAuthorizedAdd = Error.Validation("Assignment.UnAuthorizedAdd",
        "You can not add an assignment to a room you are not its doctor");

    public static readonly Error RoomHasNoDoctor = Error.Validation("Assignment.RoomHasNoDoctor",
        "You can not add an assignment to a room that dose not has a doctor");

    public static readonly Error AssignmentHasNoDoctor = Error.Validation(
        "Assignment.AssignmentHasNoDoctor",
        "You can not perform this operation on assignments that has no doctor");

    public static readonly Error UnAuthorizedDelete = Error.Validation("Assignment.UnAuthorizedDelete",
        "You can not delete an assignment you not created");

    public static readonly Error UnAuthorizedEdit = Error.Validation("Assignment.UnAuthorizedEdit",
        "You can not edit an assignment you not created");

    public static readonly Error CanNotChangeMode = Error.Validation("Assignment.CanNotChangeMode",
        "You can not change the assignment mode if it has at least one answer");

    public static readonly Error OperationIsOff = Error.Validation("Assignment.RegistrationIsOff",
        "You can not make this operation while the operations state of the room is off");

    public static readonly Error StudentIsInThisGroup = Error.Conflict("Assingment.StudentIsInThisGroup",
        "The student is already in this group");

    public static readonly Error UnAuthorizedAddFile = Error.Validation("Assignment.UnAuthorizedAddFile",
        "You can not add a file to an assignment you not created");

    public static readonly Error UnAuthorizedDeleteFile = Error.Validation("Assignment.UnAuthorizedDeleteFile",
        "You can not delete a file from an assignment you not created");

    public static readonly Error AlreadyAnswered = Error.Validation("Assignment.AlreadyAnswered",
        "You are already answered this assignment before");

    public static readonly Error DecreaseOpenDate = Error.Validation("Assignment.DecreaseOpenDate",
        "You can not set the open until date before the current open until date");

    public static readonly Error UnAuthorizedDeleteAnswer = Error.Validation("Assingment.UnAuthorizedDeleteAnswer",
        "You can not delete another one answer");

    public static readonly Error AnswerNotFound = Error.NotFound("Assignment.AnswerNotFound",
        $"There are no answer with for the given assignment");

    public static Error UserIsInAnotherGroup(string userName)
    {
        return Error.Validation("Group.UserIsInAnotherGroup",
            $"The user {userName} is in another group.");
    }

    public static Error UserIsInAnotherGroup(Guid id)
    {
        return Error.Validation("Group.UserIsInAnotherGroup",
            $"The user with id {id} is in another group.");
    }

    public static Error GroupNotFound(Guid groupId)
    {
        return Error.NotFound("Group.NotFound",
            $"There is no group with id '{groupId}'");
    }

    public static readonly Error UserIsNotInAnyGroup = Error.NotFound("Group.UserIsNotInAnyGroup",
        $"The user is not in any group for the given room");

    public static Error UserIsNotInAnyGroupInRoom(string roomName)
    {
        return Error.NotFound("Group.UserIsNotInAnyGroupInRoom",
            $"The user is not in any group for the given room '{roomName}'");
    }

    public static Error NotFound(Guid assignmentId)
    {
        return Error.NotFound("Assignment.NotFound",
            $"There is no assignment with id '{assignmentId}'");
    }

    public static Error MaxLengthExceeds(byte max)
    {
        return Error.Validation("Assignment.MaxLengthExceeds", "" +
                                                               $"You exceeds the max group length {max}");
    }

    public static Error FileNotFound(Guid id)
    {
        return Error.NotFound("Assignment.FileNotFound",
            $"There is no file with id '{id}'");
    }

    public static Error SubmitTimeEnd(DateTime openUntilDate)
    {
        return Error.Validation("Assignment.SubmitTimeEnd",
            $"This assignment can not be submit its answers after {openUntilDate:yyyy-M-d dddd}");
    }
}