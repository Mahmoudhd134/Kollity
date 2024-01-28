using Microsoft.EntityFrameworkCore;

namespace Application.Commands.Student.DeleteStudent;

public class DeleteStudentCommandHandler : ICommandHandler<DeleteStudentCommand>
{
    private readonly ApplicationDbContext _context;

    public DeleteStudentCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        var result = await _context.Students
            .Where(x => x.Id == request.Id)
            .ExecuteDeleteAsync(cancellationToken);

        return result > 0 ? Result.Success() : Error.UnKnown;
    }
}