﻿namespace Kollity.Services.Application.Commands.Room.Messages.DeletePollSubmission;

public record DeleteRoomChatPollSubmissionCommand(Guid PollId) : ICommand;