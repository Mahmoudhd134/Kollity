using Kollity.Application.Dtos.Identity;

namespace Kollity.Application.Queries.Identity.GetMySettings;

public record GetMyUserSettingsQuery() : IQuery<UserSettingsDto>;