using Kollity.Domain.ErrorHandlers;

namespace Kollity.Domain.AssignmentModels;

public static class AssignmentErrors
{
    public static Error UserIsInAnotherGroup(string userName) => Error.Validation("Group.UserIsInAnotherGroup",
        $"The user {userName} is in another group.");

    public static Error UserIsInAnotherGroup(Guid id) => Error.Validation("Group.UserIsInAnotherGroup",
        $"The user with id {id} is in another group.");

    public static readonly Error UserIsNotInTheGroup = Error.Validation("Group.UserIsNotIn",
        "The user is not in this group.");

    public static Error GroupNotFound(Guid groupId) => Error.NotFound("Group.NotFound",
        $"There is no group with id '{groupId}'");

    public static readonly Error StudentIsInAnotherGroup = Error.Validation("Group.StudentIsInAnotherGroup",
        "The student can not join more than one group within the same room in the same time");

    public static readonly Error StudentIsWaitingOnThisGroup = Error.Conflict("Assignment.StudentIsWaitingOnThisGroup",
        "The student is already waiting to join this group");
}