using Kollity.Application.Abstractions;
using Kollity.Application.Abstractions.Files;
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Identity.ChangeImagePhoto;

public class ChangeUserProfileImageCommandHandler : ICommandHandler<ChangeUserProfileImageCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IProfileImageAccessor _profileImageAccessor;
    private readonly IUserAccessor _userAccessor;

    public ChangeUserProfileImageCommandHandler(ApplicationDbContext context,
        IProfileImageAccessor profileImageAccessor,
        IUserAccessor userAccessor)
    {
        _context = context;
        _profileImageAccessor = profileImageAccessor;
        _userAccessor = userAccessor;
    }

    public async Task<Result> Handle(ChangeUserProfileImageCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine("here");
        var id = _userAccessor.GetCurrentUserId();
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

        if (string.IsNullOrWhiteSpace(oldImage) == false) await _profileImageAccessor.DeleteImage(oldImage);

        var path = await _profileImageAccessor.UploadImage(newImage, extension);
        var result = await _context.Users
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync(calls => calls
                .SetProperty(x => x.ProfileImage, path), cancellationToken);
        return result > 0 ? Result.Success() : Error.UnKnown;
    }
}