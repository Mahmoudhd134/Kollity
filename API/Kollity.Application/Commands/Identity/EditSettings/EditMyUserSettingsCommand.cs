using Kollity.Application.Dtos.Identity;

namespace Kollity.Application.Commands.Identity.EditSettings;

public record EditMyUserSettingsCommand(UserSettingsDto Dto) : ICommand;