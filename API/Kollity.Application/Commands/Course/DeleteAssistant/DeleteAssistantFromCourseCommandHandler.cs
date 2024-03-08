using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Course.DeleteAssistant;

public class DeleteAssistantFromCourseCommandHandler : ICommandHandler<DeleteAssistantFromCourseCommand>
{
    private readonly ApplicationDbContext _context;

    public DeleteAssistantFromCourseCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(DeleteAssistantFromCourseCommand request, CancellationToken cancellationToken)
    {
        var assistantId = request.CourseDoctorIdsMap.DoctorId;
        var courseId = request.CourseDoctorIdsMap.CourseId;

        var isAssistantAssigned = await _context.CourseAssistants
            .AnyAsync(x => x.AssistantId == assistantId && x.CourseId == courseId, cancellationToken);
        if (isAssistantAssigned == false)
            return CourseErrors.AssistantNotAssigned(assistantId);

        var result = await _context.CourseAssistants
            .Where(x => x.CourseId == courseId && x.AssistantId == assistantId)
            .ExecuteDeleteAsync(cancellationToken);

        return result > 0 ? Result.Success() : Error.UnKnown;
    }
}