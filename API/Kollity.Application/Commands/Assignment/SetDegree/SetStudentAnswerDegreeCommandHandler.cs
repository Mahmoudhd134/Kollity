using Kollity.Application.Abstractions;
using Kollity.Application.Abstractions.Services;
using Kollity.Application.Events;
using Kollity.Application.Events.Assignment.DegreeSet;
using Kollity.Domain.AssignmentModels;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Assignment.SetDegree;

public class SetStudentAnswerDegreeCommandHandler : ICommandHandler<SetStudentAnswerDegreeCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserServices _userServices;
    private readonly EventCollection _eventCollection;

    public SetStudentAnswerDegreeCommandHandler(ApplicationDbContext context, IUserServices userServices,
        EventCollection eventCollection)
    {
        _context = context;
        _userServices = userServices;
        _eventCollection = eventCollection;
    }

    public async Task<Result> Handle(SetStudentAnswerDegreeCommand request, CancellationToken cancellationToken)
    {
        Guid userId = _userServices.GetCurrentUserId(),
            answerId = request.Dto.AnswerId,
            studentId = request.Dto.StudentId;

        var answer = await _context.AssignmentAnswers
            .AsNoTracking()
            .Where(x => x.Id == answerId)
            .FirstOrDefaultAsync(cancellationToken);

        if (answer == null)
            return AssignmentErrors.AnswerNotFound;

        var assignment = await _context.Assignments
            .Where(x => x.Id == answer.AssignmentId)
            .Select(x => new
            {
                x.DoctorId,
                x.Degree
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (assignment is null || assignment.DoctorId == Guid.Empty)
            return AssignmentErrors.AssignmentHasNoDoctor;

        if (assignment.DoctorId != userId)
            return AssignmentErrors.UnAuthorizedAddDegree;

        if (assignment.Degree < request.Dto.StudentDegree)
            return AssignmentErrors.AssignmentDegreeOutOfRange(assignment.Degree);

        int result;
        if (answer.StudentId != null) // individual assignment
        {
            if (answer.StudentId != studentId)
                return AssignmentErrors.StudentDisMatch;
            result = await _context.AssignmentAnswers
                .Where(x => x.Id == answerId)
                .ExecuteUpdateAsync(c =>
                    c.SetProperty(x => x.Degree, request.Dto.StudentDegree), cancellationToken);
            if (result != 0)
            {
                _eventCollection.Raise(new StudentAssignmentDegreeSetEvent(answer.AssignmentId, studentId,
                    request.Dto.StudentDegree, DateTime.UtcNow));
                return Result.Success();
            }
        }

        var isStudentInGroup = await _context.AssignmentGroupStudents.AnyAsync(
            x => x.AssignmentGroupId == answer.AssignmentGroupId && x.StudentId == studentId &&
                 x.JoinRequestAccepted, cancellationToken);
        if (isStudentInGroup == false)
            return AssignmentErrors.StudentIsNotInTheGroup;

        result = await _context.AssignmentAnswerDegrees
            .Where(x =>
                x.GroupId == answer.AssignmentGroupId &&
                x.StudentId == studentId &&
                x.AnswerId == answerId)
            .ExecuteUpdateAsync(c =>
                c.SetProperty(x => x.Degree, request.Dto.StudentDegree), cancellationToken);

        if (result != 0)
        {
            _eventCollection.Raise(new StudentAssignmentDegreeSetEvent(answer.AssignmentId, studentId,
                request.Dto.StudentDegree, DateTime.UtcNow));
            return Result.Success();
        }

        var answerDegree = new AssignmentAnswerDegree()
        {
            AnswerId = answerId,
            StudentId = studentId,
            GroupId = answer.AssignmentGroupId ?? Guid.Empty,
            AssignmentId = answer.AssignmentId,
            Degree = request.Dto.StudentDegree,
        };

        _context.AssignmentAnswerDegrees.Add(answerDegree);

        result = await _context.SaveChangesAsync(cancellationToken);
        if (result == 0)
            return Error.UnKnown;

        _eventCollection.Raise(new StudentAssignmentDegreeSetEvent(answer.AssignmentId, studentId,
            request.Dto.StudentDegree, DateTime.UtcNow));
        return Result.Success();
    }
}