using Kollity.Services.Application.Dtos;
using Kollity.Services.Domain.Errors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Queries.Room.GetSingleContent;

public class GetRoomSingleContentQueryHandler : IQueryHandler<GetRoomSingleContentQuery, FileStreamDto>
{
    private readonly ApplicationDbContext _context;
    private readonly IFileServices _fileServices;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public GetRoomSingleContentQueryHandler(IFileServices fileServices, ApplicationDbContext context,
        IWebHostEnvironment webHostEnvironment)
    {
        _fileServices = fileServices;
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

        var file = await _fileServices.GetStream(Path.Combine(_webHostEnvironment.WebRootPath, content.FilePath));
        return new FileStreamDto
        {
            Name = content.Name,
            Size = file.Size,
            Stream = file.Stream,
            Extension = Path.GetExtension(content.FilePath)
        };
    }
}