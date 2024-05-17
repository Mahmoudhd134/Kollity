using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Courses;
using Kollity.Services.Domain.Errors;
using Kollity.Services.Domain.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Course.AssignDoctor;

public class AssignDoctorToCourseCommandHandler : ICommandHandler<AssignDoctorToCourseCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserServices _userServices;
    private readonly EventCollection _eventCollection;

    public AssignDoctorToCourseCommandHandler(ApplicationDbContext context, IUserServices userServices,
        EventCollection eventCollection)
    {
        _context = context;
        _userServices = userServices;
        _eventCollection = eventCollection;
    }

    public async Task<Result> Handle(AssignDoctorToCourseCommand request, CancellationToken cancellationToken)
    {
        var doctorId = request.CourseDoctorIdsMap.DoctorId;
        var courseId = request.CourseDoctorIdsMap.CourseId;

        var doctor = await _context.Doctors.FirstOrDefaultAsync(x => x.Id == doctorId, cancellationToken);
        if (doctor is null)
            return DoctorErrors.IdNotFound(doctorId);
        
        if (doctor.UserType != UserType.Doctor)
            return CourseErrors.NonDoctorAssignation;

        var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == courseId, cancellationToken);
        if (course is null)
            return CourseErrors.IdNotFound(courseId);

        if (course.DoctorId != null)
            return CourseErrors.HasAnAssignedDoctor;
        
        course.DoctorId = doctorId;
        var result = await _context.SaveChangesAsync(cancellationToken);
        if (result == 0)
            return Error.UnKnown;
        _eventCollection.Raise(new CourseDoctorAssignedEvent(course));
        return Result.Success();
    }
}