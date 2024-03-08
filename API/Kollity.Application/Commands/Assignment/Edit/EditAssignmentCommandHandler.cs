using Kollity.Application.Abstractions;
<<<<<<< HEAD
=======
using Kollity.Application.Abstractions.Services;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Assignment.Edit;

public class EditAssignmentCommandHandler : ICommandHandler<EditAssignmentCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
<<<<<<< HEAD
    private readonly IUserAccessor _userAccessor;

    public EditAssignmentCommandHandler(ApplicationDbContext context, IUserAccessor userAccessor, IMapper mapper)
    {
        _context = context;
        _userAccessor = userAccessor;
=======
    private readonly IUserServices _userServices;

    public EditAssignmentCommandHandler(ApplicationDbContext context, IUserServices userServices, IMapper mapper)
    {
        _context = context;
        _userServices = userServices;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
        _mapper = mapper;
    }

    public async Task<Result> Handle(EditAssignmentCommand request, CancellationToken cancellationToken)
    {
        Guid assignmentId = request.EditAssignmentDto.Id,
<<<<<<< HEAD
            userId = _userAccessor.GetCurrentUserId();
=======
            userId = _userServices.GetCurrentUserId();
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e

        var assignment = await _context.Assignments
            .Where(x => x.Id == assignmentId)
            .FirstOrDefaultAsync(cancellationToken);

        if (assignment is null)
            return AssignmentErrors.NotFound(assignmentId);
        if (assignment.DoctorId == Guid.Empty)
            return AssignmentErrors.AssignmentHasNoDoctor;
        if (assignment.DoctorId != userId)
            return AssignmentErrors.UnAuthorizedEdit;

        if (assignment.Mode != request.EditAssignmentDto.Mode ||
            assignment.Degree != request.EditAssignmentDto.Degree)
        {
            var hasAnswers = await _context.AssignmentAnswers
                .AnyAsync(x => x.AssignmentId == assignmentId, cancellationToken);
            if (hasAnswers)
                return AssignmentErrors.CanNotChangeIfAnswered(nameof(assignment.Mode), nameof(assignment.Degree));
        }

        if (assignment.OpenUntilDate > request.EditAssignmentDto.OpenUntilDate)
            return AssignmentErrors.DecreaseOpenDate;

<<<<<<< HEAD
=======
        request.EditAssignmentDto.OpenUntilDate = request.EditAssignmentDto.OpenUntilDate.ToUniversalTime();
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
        _mapper.Map(request.EditAssignmentDto, assignment);
        assignment.LastUpdateDate = DateTime.UtcNow;
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}