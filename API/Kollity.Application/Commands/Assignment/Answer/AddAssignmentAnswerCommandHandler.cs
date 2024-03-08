using Kollity.Application.Abstractions;
using Kollity.Application.Abstractions.Files;
<<<<<<< HEAD
=======
using Kollity.Application.Abstractions.Services;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
using Kollity.Domain.AssignmentModels;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Assignment.Answer;

public class AddAssignmentAnswerCommandHandler : ICommandHandler<AddAssignmentAnswerCommand>
{
    private readonly ApplicationDbContext _context;
<<<<<<< HEAD
    private readonly IFileAccessor _fileAccessor;
    private readonly IUserAccessor _userAccessor;

    public AddAssignmentAnswerCommandHandler(ApplicationDbContext context, IFileAccessor fileAccessor,
        IUserAccessor userAccessor)
    {
        _context = context;
        _fileAccessor = fileAccessor;
        _userAccessor = userAccessor;
=======
    private readonly IFileServices _fileServices;
    private readonly IUserServices _userServices;

    public AddAssignmentAnswerCommandHandler(ApplicationDbContext context, IFileServices fileServices,
        IUserServices userServices)
    {
        _context = context;
        _fileServices = fileServices;
        _userServices = userServices;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
    }

    public async Task<Result> Handle(AddAssignmentAnswerCommand request, CancellationToken cancellationToken)
    {
<<<<<<< HEAD
        Guid userId = _userAccessor.GetCurrentUserId(),
            assignmentId = request.AssignmentId;
        var username = _userAccessor.GetCurrentUserUserName();
=======
        Guid userId = _userServices.GetCurrentUserId(),
            assignmentId = request.AssignmentId;
        var username = _userServices.GetCurrentUserUserName();
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
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

<<<<<<< HEAD
            path = await _fileAccessor.UploadFile(file, Category.AssignmentAnswer);
=======
            path = await _fileServices.UploadFile(file, Category.AssignmentAnswer);
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
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
<<<<<<< HEAD
            await _fileAccessor.Delete(path);
=======
            await _fileServices.Delete(path);
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
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

<<<<<<< HEAD
        path = await _fileAccessor.UploadFile(file, Category.AssignmentAnswer);
=======
        path = await _fileServices.UploadFile(file, Category.AssignmentAnswer);
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
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
<<<<<<< HEAD
        await _fileAccessor.Delete(path);
=======
        await _fileServices.Delete(path);
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
        return Error.UnKnown;
    }
}