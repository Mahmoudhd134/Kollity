using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Courses;
using Kollity.Services.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Course.DeleteAssistant;

public class DeleteAssistantFromCourseCommandHandler : ICommandHandler<DeleteAssistantFromCourseCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly EventCollection _eventCollection;

    public DeleteAssistantFromCourseCommandHandler(ApplicationDbContext context, EventCollection eventCollection)
    {
        _context = context;
        _eventCollection = eventCollection;
    }

    public async Task<Result> Handle(DeleteAssistantFromCourseCommand request, CancellationToken cancellationToken)
    {
        var assistantId = request.CourseDoctorIdsMap.DoctorId;
        var courseId = request.CourseDoctorIdsMap.CourseId;

        var courseAssistant = await _context.CourseAssistants
            .FirstOrDefaultAsync(x => x.AssistantId == assistantId && x.CourseId == courseId, cancellationToken);
        if (courseAssistant is null)
            return CourseErrors.AssistantNotAssigned(assistantId);

        _context.CourseAssistants.Remove(courseAssistant);
        var result = await _context.SaveChangesAsync(cancellationToken);
        if (result == 0)
            return Error.UnKnown;

        _eventCollection.Raise(new CourseAssistantDeAssignedEvent(courseAssistant));
        return Result.Success();
    }
}