using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Assignment;
using Kollity.Services.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Assignment.Edit;

public class EditAssignmentCommandHandler : ICommandHandler<EditAssignmentCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly EventCollection _eventCollection;
    private readonly IUserServices _userServices;

    public EditAssignmentCommandHandler(ApplicationDbContext context, IUserServices userServices, IMapper mapper,
        EventCollection eventCollection)
    {
        _context = context;
        _userServices = userServices;
        _mapper = mapper;
        _eventCollection = eventCollection;
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
        var result = await _context.SaveChangesAsync(cancellationToken);
        if (result == 0)
            return Error.UnKnown;

        _eventCollection.Raise(new AssignmentEditedEvent(assignment));
        return Result.Success();
    }
}