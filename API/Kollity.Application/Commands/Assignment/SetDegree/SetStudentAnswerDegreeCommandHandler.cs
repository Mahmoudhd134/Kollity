using Kollity.Application.Abstractions;
using Kollity.Contracts.Dto;
using Kollity.Contracts.Events.Assignment;
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
                student = await _context.Students
                    .Where(x => x.Id == studentId && x.EnabledEmailNotifications)
                    .Select(x => new UserEmailDto()
                    {
                        FullName = x.FullNameInArabic,
                        Email = x.Email
                    })
                    .FirstOrDefaultAsync(cancellationToken);
                _eventCollection.Raise(new StudentAssignmentDegreeSetEvent(
                    answer.AssignmentId,
                    assignment.RoomId,
                    assignment.Name,
                    student,
                    request.Dto.StudentDegree,
                    DateTime.UtcNow));
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
            student = await _context.Students
                .Where(x => x.Id == studentId && x.EnabledEmailNotifications)
                .Select(x => new UserEmailDto()
                {
                    FullName = x.FullNameInArabic,
                    Email = x.Email
                })
                .FirstOrDefaultAsync(cancellationToken);
            _eventCollection.Raise(new StudentAssignmentDegreeSetEvent(
                answer.AssignmentId,
                assignment.RoomId,
                assignment.Name,
                student,
                request.Dto.StudentDegree,
                DateTime.UtcNow));
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

        student = await _context.Students
            .Where(x => x.Id == studentId && x.EmailConfirmed && x.EnabledEmailNotifications)
            .Select(x => new UserEmailDto()
            {
                FullName = x.FullNameInArabic,
                Email = x.Email
            })
            .FirstOrDefaultAsync(cancellationToken);
        _eventCollection.Raise(new StudentAssignmentDegreeSetEvent(
            answer.AssignmentId,
            assignment.RoomId,
            assignment.Name,
            student,
            request.Dto.StudentDegree,
            DateTime.UtcNow));
        return Result.Success();
    }
}