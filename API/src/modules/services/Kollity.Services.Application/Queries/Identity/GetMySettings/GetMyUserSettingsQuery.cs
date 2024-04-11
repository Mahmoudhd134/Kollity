using Kollity.Services.Application.Dtos.Identity;

namespace Kollity.Services.Application.Queries.Identity.GetMySettings;

public record GetMyUserSettingsQuery() : IQuery<UserSettingsDto>;