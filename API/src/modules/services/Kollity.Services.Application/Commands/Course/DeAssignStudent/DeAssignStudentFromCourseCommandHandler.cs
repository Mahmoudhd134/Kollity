using Kollity.Services.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Course.DeAssignStudent;

public class DeAssignStudentFromCourseCommandHandler : ICommandHandler<DeAssignStudentFromCourseCommand>
{
    private readonly ApplicationDbContext _context;

    public DeAssignStudentFromCourseCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(DeAssignStudentFromCourseCommand request, CancellationToken cancellationToken)
    {
        Guid sid = request.Ids.StudentId,
            cid = request.Ids.CourseId;

        var id = await _context.StudentCourses
            .Where(x => x.StudentId == sid && x.CourseId == cid)
            .Select(x => x.Id)
            .FirstOrDefaultAsync(cancellationToken);
        if (id == Guid.Empty)
            return CourseErrors.StudentIsNotAssignedToThisCourse;

        await _context.StudentCourses
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync(cancellationToken);

        return Result.Success();
    }
}