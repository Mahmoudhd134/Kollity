using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Courses;
using Kollity.Services.Domain.CourseModels;
using Kollity.Services.Domain.Errors;
using Kollity.Services.Domain.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Course.AddAssistant;

public class AddAssistantToCourseCommandHandler : ICommandHandler<AddAssistantToCourseCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserServices _userServices;
    private readonly EventCollection _eventCollection;

    public AddAssistantToCourseCommandHandler(ApplicationDbContext context, IUserServices userServices,
        EventCollection eventCollection)
    {
        _context = context;
        _userServices = userServices;
        _eventCollection = eventCollection;
    }

    public async Task<Result> Handle(AddAssistantToCourseCommand request, CancellationToken cancellationToken)
    {
        var assistantId = request.CourseDoctorIdsMap.DoctorId;
        var courseId = request.CourseDoctorIdsMap.CourseId;

        var doctor = await _context.Doctors.FirstOrDefaultAsync(x => x.Id == assistantId, cancellationToken);
        if (doctor is null)
            return DoctorErrors.IdNotFound(assistantId);

        var course = await _context.Courses
            .FirstOrDefaultAsync(x => x.Id == courseId, cancellationToken);
        if (course is null)
            return CourseErrors.IdNotFound(courseId);

        var isAssigned = await _context.CourseAssistants
            .AnyAsync(x => x.AssistantId == assistantId && x.CourseId == courseId,
                cancellationToken);
        if (isAssigned)
            return CourseErrors.AssistantAlreadyAssigned;

        var isInAssistantRole = _userServices.IsInRole(Role.Assistant);
        if (isInAssistantRole == false)
            return CourseErrors.NonAssistantAssignation;

        var courseAssistant = new CourseAssistant
        {
            AssistantId = assistantId,
            CourseId = courseId
        };
        course.CoursesAssistants.Add(courseAssistant);

        var result = await _context.SaveChangesAsync(cancellationToken);

        if (result == 0)
            return Error.UnKnown;
        _eventCollection.Raise(new CourseAssistantAssignedEvent(courseAssistant));
        return Result.Success();
    }
}