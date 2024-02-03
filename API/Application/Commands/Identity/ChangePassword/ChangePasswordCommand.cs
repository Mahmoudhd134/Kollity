using Application.Dtos.Identity;

namespace Application.Commands.Identity.ChangePassword;

public record ChangePasswordCommand(ChangePasswordDto ChangePasswordDto) : ICommand;