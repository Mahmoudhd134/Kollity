using Kollity.Application.Dtos.Identity;

namespace Kollity.Application.Commands.Identity.ChangePassword;

public record ChangePasswordCommand(ChangePasswordDto ChangePasswordDto) : ICommand;