using Kollity.Services.Domain.ErrorHandlers.Abstractions;
using Kollity.Services.Domain.ErrorHandlers.Errors;
using Kollity.Services.Application.Abstractions.Messages;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Course.DeAssignDoctor;

public class DeAssignDoctorFromCourseCommandHandler : ICommandHandler<DeAssignDoctorFromCourseCommand>
{
    private readonly ApplicationDbContext _context;

    public DeAssignDoctorFromCourseCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(DeAssignDoctorFromCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _context.Courses
            .FirstOrDefaultAsync(x => x.Id == request.CourseId, cancellationToken);

        if (course is null)
            return CourseErrors.IdNotFound(request.CourseId);

        course.DoctorId = null;
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}