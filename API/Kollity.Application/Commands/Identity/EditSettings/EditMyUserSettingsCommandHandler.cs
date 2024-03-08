using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Identity.EditSettings;

public class EditMyUserSettingsCommandHandler : ICommandHandler<EditMyUserSettingsCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserServices _userServices;
    private readonly IMapper _mapper;

    public EditMyUserSettingsCommandHandler(ApplicationDbContext context, IUserServices userServices)
    {
        _context = context;
        _userServices = userServices;
    }

    public async Task<Result> Handle(EditMyUserSettingsCommand request, CancellationToken cancellationToken)
    {
        var id = _userServices.GetCurrentUserId();
        var settings = request.Dto;

        var result = await _context.Users
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync(c =>
                c.SetProperty(x => x.EnabledEmailNotifications, settings.EnableEmailNotifications), cancellationToken);

        return Result.Success();
    }
}