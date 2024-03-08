using Kollity.Application.Abstractions.Files;
<<<<<<< HEAD
=======
using Kollity.Application.Abstractions.Services;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
using Kollity.Application.Dtos;
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Queries.Room.GetSingleContent;

public class GetRoomSingleContentQueryHandler : IQueryHandler<GetRoomSingleContentQuery, FileStreamDto>
{
    private readonly ApplicationDbContext _context;
<<<<<<< HEAD
    private readonly IFileAccessor _fileAccessor;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public GetRoomSingleContentQueryHandler(IFileAccessor fileAccessor, ApplicationDbContext context,
        IWebHostEnvironment webHostEnvironment)
    {
        _fileAccessor = fileAccessor;
=======
    private readonly IFileServices _fileServices;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public GetRoomSingleContentQueryHandler(IFileServices fileServices, ApplicationDbContext context,
        IWebHostEnvironment webHostEnvironment)
    {
        _fileServices = fileServices;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<Result<FileStreamDto>> Handle(GetRoomSingleContentQuery request,
        CancellationToken cancellationToken)
    {
        var id = request.ContentId;
        var content = await _context.RoomContents
            .Where(x => x.Id == id)
            .Select(x =>
                new
                {
                    x.FilePath,
                    x.Name
                })
            .FirstOrDefaultAsync(cancellationToken);

        if (content is null)
            return RoomErrors.ContentIdNotFound(id);

<<<<<<< HEAD
        var file = await _fileAccessor.GetStream(Path.Combine(_webHostEnvironment.WebRootPath, content.FilePath));
=======
        var file = await _fileServices.GetStream(Path.Combine(_webHostEnvironment.WebRootPath, content.FilePath));
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
        return new FileStreamDto
        {
            Name = content.Name,
            Size = file.Size,
            Stream = file.Stream,
            Extension = Path.GetExtension(content.FilePath)
        };
    }
}