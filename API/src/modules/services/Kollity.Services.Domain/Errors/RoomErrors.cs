﻿using Kollity.Common.ErrorHandling;

namespace Kollity.Services.Domain.Errors;

public static class RoomErrors
{
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

    public static readonly Error UnAuthorizeAddSupervisor = Error.Validation("Room.UnAuthorizeAddSupervisor",
        "You do not have the right permissions to add a new supervisor to this room.");

    public static readonly Error UnAuthorizeDeleteSupervisor = Error.Validation("Room.UnAuthorizeDeleteSupervisor",
        "You do not have the right permissions to delete an supervisor from this room.");

    public static readonly Error DoctorMustBeAnSupervisor = Error.Validation("Room.DoctorMustBeAnSupervisor",
        "The room doctor must be an supervisor");

    public static readonly Error UnAuthorizeAddContent = Error.Validation("Room.UnAuthorizedAddContent",
        "You can not add content to a room you are not in a supervisor position");

    public static readonly Error UnAuthorizeDeleteContent = Error.Validation("Room.UnAuthorizedDeleteContent",
        "You can not delete content from a room you are not in a supervisor position");

    public static readonly Error RoomHasNoDoctor = Error.Validation("Room.RoomHasNoDoctor",
        "You can not perform this action of a room that has no doctor");

    public static readonly Error UnAuthorizeAddExam = Error.Validation("Room.UnAuthorizeAddExam",
        "You can not add an exam to a room you not its doctor");

    public static readonly Error UnAuthorizeAddMessage = Error.Validation("Room.UnAuthorizeAddMessage",
        "You can not add a message to a room you are no joined");

    public static readonly Error PollInvalidQuestionLength =
        Error.Validation("Poll.InvalidQuestionLength", "Question length exceeds 500 characters.");

    public static readonly Error PollInvalidOptionsCount =
        Error.Validation("Poll.InvalidOptionsCount", "Invalid number of options it must be in range of 1 and 10.");

    public static readonly Error PollInvalidOptionLength =
        Error.Validation("Poll.InvalidOptionLength", "Option length exceeds 300 characters.");

    public static readonly Error PollAlreadySubmitted = Error.Validation("Poll.AlreadySubmitted",
        "you already submitted this poll before");

    public static readonly Error PollOptionNotFound = Error.NotFound("Poll.OptionNotFound",
        "This option is not found");

    public static readonly Error PollIsNotMultiAnswer = Error.Validation("Poll.PollIsNotMultiAnswer",
        "This poll is not a multi answer poll");

    public static readonly Error PollInvalidMaxOptionsCount = Error.Validation("Poll.InvalidMaxOptionsCount",
        "Max options count is invalid");


    public static Error NotFound(Guid roomId)
    {
        return Error.NotFound("Room.NotFound", $"The id '{roomId}' is wrong.");
    }

    public static Error NotFound(string roomName)
    {
        return Error.NotFound("Room.NotFound", $"The name '{roomName}' is wrong.");
    }

    public static Error ContentIdNotFound(Guid contentId)
    {
        return Error.NotFound("RoomContent.WrongId", $"The id '{contentId}' is wrong.");
    }

    public static Error UserIsNotJoined(string userName)
    {
        return Error.Validation("Room.UserIdNotJoined",
            $"There are no user with user name '{userName} in this room.");
    }

    public static Error UserIsNotJoined(Guid userId)
    {
        return Error.Validation("Room.UserIdNotJoined",
            $"There are no user with id '{userId} in this room.");
    }

    public static Error MessageNotFound(Guid messageId)
    {
        return Error.NotFound("Room.MessageNotFound", $"there is no message you sent with id {messageId}");
    }

    public static Error PollSubmissionNotFound(Guid pollId)
    {
        return Error.NotFound("Poll.SubmissionNotFound",
            $"this user dose not submit the poll with id '{pollId}' to delete its submission");
    }

    public static Error PollAnswersLimitExceeds(byte maxOptionsCountForSubmission)
    {
        return Error.Validation("Poll.PollAnswersLimitExceeds",
            $"This multi answer poll ans a max limit of {maxOptionsCountForSubmission}");
    }
}