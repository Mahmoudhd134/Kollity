namespace Kollity.Services.Application.Commands.Room.Messages.SubmitPoll;

public record SubmitRoomChatMessagePollCommand(Guid PollId, List<byte> OptionIndexes) : ICommand;