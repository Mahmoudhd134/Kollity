using Kollity.Application.Abstractions.Files;
using Kollity.Application.Dtos;
using Kollity.Application.Dtos.Assignment;
using Kollity.Domain.AssignmentModels;
using Kollity.Domain.RoomModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Queries.Assignment.GetFile;

public class GetAssignmentFileQueryHandler : IQueryHandler<GetAssignmentFileQuery, FileStreamDto>
{
    private readonly ApplicationDbContext _context;
    private readonly IFileAccessor _fileAccessor;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public GetAssignmentFileQueryHandler(ApplicationDbContext context, IFileAccessor fileAccessor,
        IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _fileAccessor = fileAccessor;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<Result<FileStreamDto>> Handle(GetAssignmentFileQuery request, CancellationToken cancellationToken)
    {
        var id = request.FileId;
        var fileDto = await _context.AssignmentFiles
            .Where(x => x.Id == id)
            .Select(x =>
                new
                {
                    x.FilePath,
                    x.Name,
                })
            .FirstOrDefaultAsync(cancellationToken);

        if (fileDto is null)
            return AssignmentErrors.FileNotFound(id);

        var file = await _fileAccessor.GetStream(Path.Combine(_webHostEnvironment.WebRootPath, fileDto.FilePath));
        return new FileStreamDto()
        {
            Name = fileDto.Name,
            Size = file.Size,
            Stream = file.Stream,
            Extension = file.Extension
        };
    }
}