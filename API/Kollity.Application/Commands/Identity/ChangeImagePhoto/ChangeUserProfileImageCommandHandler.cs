using Kollity.Application.Abstractions;
using Kollity.Application.Abstractions.Files;
using Kollity.Domain.Identity.User;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Identity.ChangeImagePhoto;

public class ChangeUserProfileImageCommandHandler : ICommandHandler<ChangeUserProfileImageCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IImageAccessor _imageAccessor;
    private readonly IUserAccessor _userAccessor;

    public ChangeUserProfileImageCommandHandler(ApplicationDbContext context,
        IImageAccessor imageAccessor,
        IUserAccessor userAccessor)
    {
        _context = context;
        _imageAccessor = imageAccessor;
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

        if (string.IsNullOrWhiteSpace(oldImage) == false) await _imageAccessor.DeleteImage(oldImage);

        var path = await _imageAccessor.UploadImage(newImage, extension);
        var result = await _context.Users
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync(calls => calls
                .SetProperty(x => x.ProfileImage, path), cancellationToken);
        return result > 0 ? Result.Success() : Error.UnKnown;
    }
}