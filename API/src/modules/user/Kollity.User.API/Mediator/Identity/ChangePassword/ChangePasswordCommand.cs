using Kollity.User.API.Abstraction.Messages;
using Kollity.User.API.Dtos.Identity;

namespace Kollity.User.API.Mediator.Identity.ChangePassword;

public record ChangePasswordCommand(ChangePasswordDto ChangePasswordDto) : ICommand;