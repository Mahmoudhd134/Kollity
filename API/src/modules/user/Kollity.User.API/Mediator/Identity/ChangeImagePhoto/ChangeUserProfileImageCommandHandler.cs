using Kollity.Common.Abstractions.Messages;
using Kollity.Common.ErrorHandling;
using Kollity.User.API.Abstraction.Services;
using Kollity.User.API.Data;
using Kollity.User.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Kollity.User.API.Mediator.Identity.ChangeImagePhoto;

public class ChangeUserProfileImageCommandHandler : ICommandHandler<ChangeUserProfileImageCommand, string>
{
    private readonly UserDbContext _context;
    private readonly IProfileImageServices _profileImageServices;
    private readonly IUserServices _userServices;

    public ChangeUserProfileImageCommandHandler(UserDbContext context,
        IProfileImageServices profileImageServices,
        IUserServices userServices)
    {
        _context = context;
        _profileImageServices = profileImageServices;
        _userServices = userServices;
    }

    public async Task<Result<string>> Handle(ChangeUserProfileImageCommand request, CancellationToken cancellationToken)
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
        return result > 0 ? path : Error.UnKnown;
    }
}