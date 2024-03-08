using Kollity.Application.Abstractions.Files;
using Kollity.Application.Abstractions.Services;
using Kollity.Application.Dtos;
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Queries.Assignment.GetFile;

public class GetAssignmentFileQueryHandler : IQueryHandler<GetAssignmentFileQuery, FileStreamDto>
{
    private readonly ApplicationDbContext _context;
    private readonly IFileServices _fileServices;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public GetAssignmentFileQueryHandler(ApplicationDbContext context, IFileServices fileServices,
        IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _fileServices = fileServices;
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
                    x.Name
                })
            .FirstOrDefaultAsync(cancellationToken);

        if (fileDto is null)
            return AssignmentErrors.FileNotFound(id);

        var file = await _fileServices.GetStream(Path.Combine(_webHostEnvironment.WebRootPath, fileDto.FilePath));
        return new FileStreamDto
        {
            Name = fileDto.Name,
            Size = file.Size,
            Stream = file.Stream,
            Extension = file.Extension
        };
    }
}