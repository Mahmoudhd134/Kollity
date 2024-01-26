using Domain;
using Domain.ErrorHandlers;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.Course.DeleteCourse;

public class DeleteCourseCommandHandler : ICommandHandler<DeleteCourseCommand>
{
    private readonly ApplicationDbContext _context;

    public DeleteCourseCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        var result = await _context.Courses
            .Where(x => x.Id == request.Id)
            .ExecuteDeleteAsync(cancellationToken);
        return result > 0 ? Result.Success() : Error.UnKnown;
    }
}