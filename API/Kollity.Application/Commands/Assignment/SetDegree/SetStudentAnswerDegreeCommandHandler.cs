using Kollity.Application.Abstractions;
using Kollity.Domain.AssignmentModels;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Assignment.SetDegree;

public class SetStudentAnswerDegreeCommandHandler : ICommandHandler<SetStudentAnswerDegreeCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserAccessor _userAccessor;

    public SetStudentAnswerDegreeCommandHandler(ApplicationDbContext context, IUserAccessor userAccessor)
    {
        _context = context;
        _userAccessor = userAccessor;
    }

    public async Task<Result> Handle(SetStudentAnswerDegreeCommand request, CancellationToken cancellationToken)
    {
        Guid userId = _userAccessor.GetCurrentUserId(),
            answerId = request.Dto.AnswerId,
            studentId = request.Dto.StudentId;

        var answer = await _context.AssignmentAnswers
            .AsNoTracking()
            .Where(x => x.Id == answerId)
            .FirstOrDefaultAsync(cancellationToken);

        if (answer == null)
            return AssignmentErrors.AnswerNotFound;

        var assignmentDoctorId = await _context.Assignments
            .Where(x => x.Id == answer.AssignmentId)
            .Select(x => x.DoctorId)
            .FirstOrDefaultAsync(cancellationToken);

        if (assignmentDoctorId is null || assignmentDoctorId == Guid.Empty)
            return AssignmentErrors.AssignmentHasNoDoctor;

        if (assignmentDoctorId != userId)
            return AssignmentErrors.UnAuthorizedAddDegree;

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
                return Result.Success();
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
            return Result.Success();

        var answerDegree = new AssignmentAnswerDegree()
        {
            AnswerId = answerId,
            StudentId = studentId,
            GroupId = answer.AssignmentGroupId ?? Guid.Empty,
            Degree = request.Dto.StudentDegree,
        };

        _context.AssignmentAnswerDegrees.Add(answerDegree);

        result = await _context.SaveChangesAsync(cancellationToken);
        return result > 0 ? Result.Success() : Error.UnKnown;
    }
}