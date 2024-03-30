﻿using Kollity.Domain.Identity.User;

namespace Kollity.Domain.RoomModels;

public class MessagePollAnswer
{
    public Guid UserId { get; set; }
    public BaseUser User { get; set; }
    public Guid PollId { get; set; }
    public RoomMessage Poll { get; set; }
    public byte OptionIndex { get; set; }
}