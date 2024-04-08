using Kollity.Services.Application.Abstractions;
using Kollity.Services.Domain.AssignmentModels;
using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Abstractions.Services;
using Kollity.Services.Application.Events.Assignment;
using Kollity.Services.Application.Events.Dto;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Assignment.SetDegree;

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
                x.RoomId,
                x.DoctorId,
                x.Degree,
                x.Name
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (assignment is null || assignment.DoctorId == Guid.Empty)
            return AssignmentErrors.AssignmentHasNoDoctor;

        if (assignment.DoctorId != userId)
            return AssignmentErrors.UnAuthorizedAddDegree;

        if (assignment.Degree < request.Dto.StudentDegree)
            return AssignmentErrors.AssignmentDegreeOutOfRange(assignment.Degree);

        int result;
        UserEmailDto student;
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
                answer.Degree = request.Dto.StudentDegree;
                _eventCollection.Raise(new StudentIndividualAssignmentDegreeSetEvent(answer));
                return Result.Success();
            }
        }

        var isStudentInGroup = await _context.AssignmentGroupStudents.AnyAsync(
            x => x.AssignmentGroupId == answer.AssignmentGroupId && x.StudentId == studentId &&
                 x.JoinRequestAccepted, cancellationToken);
        if (isStudentInGroup == false)
            return AssignmentErrors.StudentIsNotInTheGroup;

        var degree = await _context.AssignmentAnswerDegrees
            .Where(x =>
                x.GroupId == answer.AssignmentGroupId &&
                x.StudentId == studentId &&
                x.AnswerId == answerId)
            .FirstOrDefaultAsync(cancellationToken);

        if (degree is not null)
        {
            degree.Degree = request.Dto.StudentDegree;
            result = await _context.SaveChangesAsync(cancellationToken);
            if (result == 0)
                return Error.UnKnown;
            _eventCollection.Raise(new StudentGroupAssignmentDegreeSetEvent(degree));
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

        _eventCollection.Raise(new StudentGroupAssignmentDegreeSetEvent(answerDegree));
        return Result.Success();
    }
}