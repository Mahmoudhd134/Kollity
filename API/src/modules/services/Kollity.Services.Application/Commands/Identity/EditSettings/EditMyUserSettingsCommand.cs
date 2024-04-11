using Kollity.Services.Application.Dtos.Identity;

namespace Kollity.Services.Application.Commands.Identity.EditSettings;

public record EditMyUserSettingsCommand(UserSettingsDto Dto) : ICommand;