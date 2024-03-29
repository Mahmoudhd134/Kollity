﻿using Kollity.Application.Abstractions.Files;
using Kollity.Application.Abstractions.Services;
using Kollity.Domain.ErrorHandlers.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Course.Delete;

public class DeleteCourseCommandHandler : ICommandHandler<DeleteCourseCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IFileServices _fileServices;

    public DeleteCourseCommandHandler(ApplicationDbContext context, IFileServices fileServices)
    {
        _context = context;
        _fileServices = fileServices;
    }

    public async Task<Result> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        var courseId = request.Id;
        var contents = await _context.Rooms
            .Where(x => x.CourseId == courseId)
            .Select(x => new
            {
                Contents = x.RoomContents.Select(xx => xx.FilePath).ToList(),
                AssingmentFiles = x.Assignments.SelectMany(xx =>
                    xx.AssignmentFiles.Select(xxx => xxx.FilePath)).ToList(),
                AssignmentAnswers = x.Assignments.SelectMany(xx =>
                    xx.AssignmentsAnswers.Select(xxx => xxx.File)).ToList()
            })
            .ToListAsync(cancellationToken);

        var result = await _context.Courses
            .Where(x => x.Id == request.Id)
            .ExecuteDeleteAsync(cancellationToken);

        if (result == 0)
            return Error.UnKnown;

        foreach (var content in contents)
        {
            await _fileServices.Delete(content.Contents);
            await _fileServices.Delete(content.AssingmentFiles);
            await _fileServices.Delete(content.AssignmentAnswers);
        }

        return Result.Success();
    }
}