using Kollity.Services.Domain.Errors;
using Kollity.Services.Domain.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Course.AssignDoctor;

public class AssignDoctorToCourseCommandHandler : ICommandHandler<AssignDoctorToCourseCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserServices _userServices;

    public AssignDoctorToCourseCommandHandler(ApplicationDbContext context, IUserServices userServices)
    {
        _context = context;
        _userServices = userServices;
    }

    public async Task<Result> Handle(AssignDoctorToCourseCommand request, CancellationToken cancellationToken)
    {
        var doctorId = request.CourseDoctorIdsMap.DoctorId;
        var courseId = request.CourseDoctorIdsMap.CourseId;

        var doctor = await _context.Doctors.FirstOrDefaultAsync(x => x.Id == doctorId, cancellationToken);
        if (doctor is null)
            return DoctorErrors.IdNotFound(doctorId);

        var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == courseId, cancellationToken);
        if (course is null)
            return CourseErrors.IdNotFound(courseId);

        if (course.DoctorId != null)
            return CourseErrors.HasAnAssignedDoctor;

        var isInDoctorRole = _userServices.IsInRole(Role.Doctor);
        if (isInDoctorRole == false)
            return CourseErrors.NonDoctorAssignation;

        course.DoctorId = doctorId;
        var result = await _context.SaveChangesAsync(cancellationToken);
        return result > 0 ? Result.Success() : Error.UnKnown;
    }
}