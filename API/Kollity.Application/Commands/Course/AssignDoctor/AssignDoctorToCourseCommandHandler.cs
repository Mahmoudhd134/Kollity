using Kollity.Domain.CourseModels;
using Kollity.Domain.DoctorModels;
using Kollity.Domain.Identity.Role;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Course.AssignDoctor;

public class AssignDoctorToCourseCommandHandler : ICommandHandler<AssignDoctorToCourseCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<Domain.DoctorModels.Doctor> _doctorManager;

    public AssignDoctorToCourseCommandHandler(ApplicationDbContext context,
        UserManager<Domain.DoctorModels.Doctor> doctorManager)
    {
        _context = context;
        _doctorManager = doctorManager;
    }

    public async Task<Result> Handle(AssignDoctorToCourseCommand request, CancellationToken cancellationToken)
    {
        var doctorId = request.CourseDoctorIdsMap.DoctorId;
        var courseId = request.CourseDoctorIdsMap.CourseId;

        var doctor = await _doctorManager.FindByIdAsync(doctorId.ToString());
        if (doctor is null)
            return DoctorErrors.IdNotFound(doctorId);

        var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == courseId, cancellationToken);
        if (course is null)
            return CourseErrors.IdNotFound(courseId);

        if (course.DoctorId != null)
            return CourseErrors.HasAnAssignedDoctor;

        var isInDoctorRole = await _doctorManager.IsInRoleAsync(doctor, Role.Doctor);
        if (isInDoctorRole == false)
            return CourseErrors.NonDoctorAssignation;

        course.DoctorId = doctorId;
        var result = await _context.SaveChangesAsync(cancellationToken);
        return result > 0 ? Result.Success() : Error.UnKnown;
    }
}