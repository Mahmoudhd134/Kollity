using Kollity.Services.Application.Dtos.Identity;
using Kollity.Services.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Queries.Identity.GetMySettings;

public class GetMyUserSettingsQueryHandler : IQueryHandler<GetMyUserSettingsQuery, UserSettingsDto>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserServices _userServices;

    public GetMyUserSettingsQueryHandler(ApplicationDbContext context, IUserServices userServices)
    {
        _context = context;
        _userServices = userServices;
    }

    public async Task<Result<UserSettingsDto>> Handle(GetMyUserSettingsQuery request, CancellationToken cancellationToken)
    {
        var id = _userServices.GetCurrentUserId();
        var userSettings = await _context.Users
            .Where(x => x.Id == id)
            .Select(x => new UserSettingsDto()
            {
                EnableEmailNotifications = x.EnabledEmailNotifications
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (userSettings is null)
            return UserErrors.IdNotFound(id);
        return userSettings;
    }
}