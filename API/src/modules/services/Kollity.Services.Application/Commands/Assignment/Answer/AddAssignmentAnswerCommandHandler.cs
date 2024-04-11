using Kollity.Services.Domain.AssignmentModels;
using Kollity.Services.Application.Abstractions.Files;
using Kollity.Services.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Assignment.Answer;

public class AddAssignmentAnswerCommandHandler : ICommandHandler<AddAssignmentAnswerCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IFileServices _fileServices;
    private readonly IUserServices _userServices;

    public AddAssignmentAnswerCommandHandler(ApplicationDbContext context, IFileServices fileServices,
        IUserServices userServices)
    {
        _context = context;
        _fileServices = fileServices;
        _userServices = userServices;
    }

    public async Task<Result> Handle(AddAssignmentAnswerCommand request, CancellationToken cancellationToken)
    {
        Guid userId = _userServices.GetCurrentUserId(),
            assignmentId = request.AssignmentId;
        var username = _userServices.GetCurrentUserUserName();
        var file = request.AddAssignmentAnswerDto.File;

        var assignment = await _context.Assignments
            .Where(x => x.Id == assignmentId)
            .Select(x => new
            {
                x.Mode,
                x.OpenUntilDate,
                x.RoomId
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (assignment is null)
            return AssignmentErrors.NotFound(assignmentId);

        if (DateTime.UtcNow > assignment.OpenUntilDate)
            return AssignmentErrors.SubmitTimeEnd(assignment.OpenUntilDate);
        
        //check that the user is in the room which the assignment belongs to
        var isInTheRoom = await _context.UserRooms
            .AnyAsync(x => x.UserId == userId && x.RoomId == assignment.RoomId && x.JoinRequestAccepted, cancellationToken);
        if (isInTheRoom == false)
            return RoomErrors.UserIsNotJoined(username);

        string path;
        if (assignment.Mode == AssignmentMode.Individual)
        {
            //check that that user didn't answer that assignment before 
            var studentDidAnswer = await _context.AssignmentAnswers
                .AnyAsync(
                    x => x.StudentId == userId && x.AssignmentId == assignmentId,
                    cancellationToken);
            if (studentDidAnswer)
                return AssignmentErrors.AlreadyAnswered;

            path = await _fileServices.UploadFile(file, Category.AssignmentAnswer);
            AssignmentAnswer assignmentAnswer = new()
            {
                File = path,
                AssignmentId = assignmentId,
                StudentId = userId,
                UploadDate = DateTime.UtcNow,
                AssignmentGroupId = null,
            };
            _context.AssignmentAnswers.Add(assignmentAnswer);
            var result = await _context.SaveChangesAsync(cancellationToken);
            if (result != 0)
                return Result.Success();
            await _fileServices.Delete(path);
            return Error.UnKnown;
        }

        var groupId = await _context.AssignmentGroups
            .Where(x => x.RoomId == assignment.RoomId &&
                        x.AssignmentGroupsStudents.Any(xx => xx.StudentId == userId && xx.JoinRequestAccepted))
            .Select(x => x.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (groupId == Guid.Empty)
            return AssignmentErrors.UserIsNotInAnyGroup;

        var groupDidAnswer = await _context.AssignmentAnswers
            .AnyAsync(
                x => x.AssignmentGroupId == groupId && x.AssignmentId == assignmentId,
                cancellationToken);
        if (groupDidAnswer)
            return AssignmentErrors.AlreadyAnswered;

        path = await _fileServices.UploadFile(file, Category.AssignmentAnswer);
        var assignmentAnswers = new AssignmentAnswer
        {
            File = path,
            AssignmentId = assignmentId,
            UploadDate = DateTime.UtcNow,
            AssignmentGroupId = groupId,
        };
        _context.AssignmentAnswers.AddRange(assignmentAnswers);
        var result1 = await _context.SaveChangesAsync(cancellationToken);
        if (result1 != 0)
            return Result.Success();
        await _fileServices.Delete(path);
        return Error.UnKnown;
    }
}