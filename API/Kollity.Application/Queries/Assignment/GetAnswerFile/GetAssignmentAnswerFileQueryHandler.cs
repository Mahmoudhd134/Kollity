using Kollity.Application.Abstractions.Files;
using Kollity.Application.Dtos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Queries.Assignment.GetAnswerFile;

public class GetAssignmentAnswerFileQueryHandler : IQueryHandler<GetAssignmentAnswerFileQuery, FileStreamDto>
{
    private readonly ApplicationDbContext _context;
    private readonly IFileAccessor _fileAccessor;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public GetAssignmentAnswerFileQueryHandler(ApplicationDbContext context, IFileAccessor fileAccessor,
        IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _fileAccessor = fileAccessor;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<Result<FileStreamDto>> Handle(GetAssignmentAnswerFileQuery request,
        CancellationToken cancellationToken)
    {
        var id = request.AnswerId;
        var fileDto = await _context.AssignmentAnswers
            .Where(x => x.Id == id)
            .Select(x => new
            {
                x.File,
                x.AssignmentId,
                StudentName = x.StudentId != null ? x.Student.FullNameInArabic : null,
                GroupCode = x.AssignmentGroupId != null ? x.AssignmentGroup.Code : -1
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (fileDto is null)
            return AssignmentErrors.FileNotFound(id);

        var assignmentName = await _context.Assignments
            .Where(x => x.Id == fileDto.AssignmentId)
            .Select(x => x.Name)
            .FirstOrDefaultAsync(cancellationToken);

        if (string.IsNullOrWhiteSpace(assignmentName))
            return AssignmentErrors.NotFound(fileDto.AssignmentId);

        var file = await _fileAccessor.GetStream(Path.Combine(_webHostEnvironment.WebRootPath, fileDto.File));
        var fileName = assignmentName + "-Answer-" +
                       (fileDto.GroupCode != -1 ? $"Group-{fileDto.GroupCode.ToString()}" : $"Student-{fileDto.StudentName}");
        return new FileStreamDto
        {
            Name = fileName,
            Size = file.Size,
            Stream = file.Stream,
            Extension = file.Extension
        };
    }
}