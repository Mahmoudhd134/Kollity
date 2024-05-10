using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Courses;
using Kollity.Services.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Course.DeAssignDoctor;

public class DeAssignDoctorFromCourseCommandHandler : ICommandHandler<DeAssignDoctorFromCourseCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly EventCollection _eventCollection;

    public DeAssignDoctorFromCourseCommandHandler(ApplicationDbContext context, EventCollection eventCollection)
    {
        _context = context;
        _eventCollection = eventCollection;
    }

    public async Task<Result> Handle(DeAssignDoctorFromCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _context.Courses
            .FirstOrDefaultAsync(x => x.Id == request.CourseId, cancellationToken);

        if (course is null)
            return CourseErrors.IdNotFound(request.CourseId);
        if (course.DoctorId is null)
            return CourseErrors.HasNoDoctor;

        var did = course.DoctorId;
        course.DoctorId = null;
        var result = await _context.SaveChangesAsync(cancellationToken);
        if (result == 0)
            return Error.UnKnown;
        _eventCollection.Raise(new CourseDoctorDeAssignedEvent(course,(Guid)did));
        return Result.Success();
    }
}