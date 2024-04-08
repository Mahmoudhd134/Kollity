using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Dtos.Identity;

namespace Kollity.Services.Application.Commands.Identity.ChangePassword;

public record ChangePasswordCommand(ChangePasswordDto ChangePasswordDto) : ICommand;