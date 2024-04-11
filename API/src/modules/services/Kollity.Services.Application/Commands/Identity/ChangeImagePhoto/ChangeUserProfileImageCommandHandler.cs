using Kollity.Services.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Identity.ChangeImagePhoto;

public class ChangeUserProfileImageCommandHandler : ICommandHandler<ChangeUserProfileImageCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IProfileImageServices _profileImageServices;
    private readonly IUserServices _userServices;

    public ChangeUserProfileImageCommandHandler(ApplicationDbContext context,
        IProfileImageServices profileImageServices,
        IUserServices userServices)
    {
        _context = context;
        _profileImageServices = profileImageServices;
        _userServices = userServices;
    }

    public async Task<Result> Handle(ChangeUserProfileImageCommand request, CancellationToken cancellationToken)
    {
        var id = _userServices.GetCurrentUserId();
        var newImage = request.ImageDto.ImageStream;
        var extension = request.ImageDto.Extensions;

        string oldImage;
        try
        {
            oldImage = await _context.Users
                .Where(x => x.Id == id)
                .Select(x => x.ProfileImage)
                .FirstAsync(cancellationToken);
        }
        catch
        {
            return UserErrors.IdNotFound(id);
        }

        if (string.IsNullOrWhiteSpace(oldImage) == false) await _profileImageServices.DeleteImage(oldImage);

        var path = await _profileImageServices.UploadImage(newImage, extension);
        var result = await _context.Users
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync(calls => calls
                .SetProperty(x => x.ProfileImage, path), cancellationToken);
        return result > 0 ? Result.Success() : Error.UnKnown;
    }
}