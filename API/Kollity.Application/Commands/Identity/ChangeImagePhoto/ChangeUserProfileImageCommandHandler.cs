using Kollity.Application.Abstractions;
using Kollity.Application.Abstractions.Files;
<<<<<<< HEAD
=======
using Kollity.Application.Abstractions.Services;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Identity.ChangeImagePhoto;

public class ChangeUserProfileImageCommandHandler : ICommandHandler<ChangeUserProfileImageCommand>
{
    private readonly ApplicationDbContext _context;
<<<<<<< HEAD
    private readonly IProfileImageAccessor _profileImageAccessor;
    private readonly IUserAccessor _userAccessor;

    public ChangeUserProfileImageCommandHandler(ApplicationDbContext context,
        IProfileImageAccessor profileImageAccessor,
        IUserAccessor userAccessor)
    {
        _context = context;
        _profileImageAccessor = profileImageAccessor;
        _userAccessor = userAccessor;
=======
    private readonly IProfileImageServices _profileImageServices;
    private readonly IUserServices _userServices;

    public ChangeUserProfileImageCommandHandler(ApplicationDbContext context,
        IProfileImageServices profileImageServices,
        IUserServices userServices)
    {
        _context = context;
        _profileImageServices = profileImageServices;
        _userServices = userServices;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
    }

    public async Task<Result> Handle(ChangeUserProfileImageCommand request, CancellationToken cancellationToken)
    {
<<<<<<< HEAD
        var id = _userAccessor.GetCurrentUserId();
=======
        var id = _userServices.GetCurrentUserId();
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
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

<<<<<<< HEAD
        if (string.IsNullOrWhiteSpace(oldImage) == false) await _profileImageAccessor.DeleteImage(oldImage);

        var path = await _profileImageAccessor.UploadImage(newImage, extension);
=======
        if (string.IsNullOrWhiteSpace(oldImage) == false) await _profileImageServices.DeleteImage(oldImage);

        var path = await _profileImageServices.UploadImage(newImage, extension);
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
        var result = await _context.Users
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync(calls => calls
                .SetProperty(x => x.ProfileImage, path), cancellationToken);
        return result > 0 ? Result.Success() : Error.UnKnown;
    }
}