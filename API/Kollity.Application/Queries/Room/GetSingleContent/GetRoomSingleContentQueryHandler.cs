using Kollity.Application.Abstractions.Files;
using Kollity.Application.Dtos;
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Queries.Room.GetSingleContent;

public class GetRoomSingleContentQueryHandler : IQueryHandler<GetRoomSingleContentQuery, FileStreamDto>
{
    private readonly ApplicationDbContext _context;
    private readonly IFileAccessor _fileAccessor;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public GetRoomSingleContentQueryHandler(IFileAccessor fileAccessor, ApplicationDbContext context,
        IWebHostEnvironment webHostEnvironment)
    {
        _fileAccessor = fileAccessor;
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

        var file = await _fileAccessor.GetStream(Path.Combine(_webHostEnvironment.WebRootPath, content.FilePath));
        return new FileStreamDto
        {
            Name = content.Name,
            Size = file.Size,
            Stream = file.Stream,
            Extension = Path.GetExtension(content.FilePath)
        };
    }
}