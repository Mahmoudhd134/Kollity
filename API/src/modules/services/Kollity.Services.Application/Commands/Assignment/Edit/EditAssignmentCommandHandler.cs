using Kollity.Services.Application.Abstractions;
using Kollity.Services.Domain.ErrorHandlers.Abstractions;
using Kollity.Services.Domain.ErrorHandlers.Errors;
using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Abstractions.Services;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Assignment.Edit;

public class EditAssignmentCommandHandler : ICommandHandler<EditAssignmentCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUserServices _userServices;

    public EditAssignmentCommandHandler(ApplicationDbContext context, IUserServices userServices, IMapper mapper)
    {
        _context = context;
        _userServices = userServices;
        _mapper = mapper;
    }

    public async Task<Result> Handle(EditAssignmentCommand request, CancellationToken cancellationToken)
    {
        Guid assignmentId = request.EditAssignmentDto.Id,
            userId = _userServices.GetCurrentUserId();

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

        request.EditAssignmentDto.OpenUntilDate = request.EditAssignmentDto.OpenUntilDate.ToUniversalTime();
        _mapper.Map(request.EditAssignmentDto, assignment);
        assignment.LastUpdateDate = DateTime.UtcNow;
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}