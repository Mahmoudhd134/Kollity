using Kollity.Application.Abstractions;
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Assignment.Edit;

public class EditAssignmentCommandHandler : ICommandHandler<EditAssignmentCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUserAccessor _userAccessor;

    public EditAssignmentCommandHandler(ApplicationDbContext context, IUserAccessor userAccessor, IMapper mapper)
    {
        _context = context;
        _userAccessor = userAccessor;
        _mapper = mapper;
    }

    public async Task<Result> Handle(EditAssignmentCommand request, CancellationToken cancellationToken)
    {
        Guid assignmentId = request.EditAssignmentDto.Id,
            userId = _userAccessor.GetCurrentUserId();

        var assignment = await _context.Assignments
            .Where(x => x.Id == assignmentId)
            .FirstOrDefaultAsync(cancellationToken);

        if (assignment is null)
            return AssignmentErrors.NotFound(assignmentId);
        if (assignment.DoctorId == Guid.Empty)
            return AssignmentErrors.AssignmentHasNoDoctor;
        if (assignment.DoctorId != userId)
            return AssignmentErrors.UnAuthorizedEdit;

        if (assignment.Mode != request.EditAssignmentDto.Mode)
        {
            var hasAnswers = await _context.AssignmentAnswers
                .AnyAsync(x => x.AssignmentId == assignmentId, cancellationToken);
            if (hasAnswers)
                return AssignmentErrors.CanNotChangeMode;
        }

        _mapper.Map(request.EditAssignmentDto, assignment);
        assignment.LastUpdateDate = DateTime.UtcNow;
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}