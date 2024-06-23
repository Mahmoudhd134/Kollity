using Kollity.Exams.Application.Abstractions.Events;
using Kollity.Exams.Application.Abstractions.Services;
using Kollity.Exams.Application.Events.ExamEvents;
using Kollity.Exams.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Exams.Application.Commands.Delete;

public class DeleteExamCommandHandler : ICommandHandler<DeleteExamCommand>
{
    private readonly ExamsDbContext _context;
    private readonly IUserServices _userServices;
    private readonly EventCollection _eventCollection;

    public DeleteExamCommandHandler(ExamsDbContext context, IUserServices userServices,
        EventCollection eventCollection)
    {
        _context = context;
        _userServices = userServices;
        _eventCollection = eventCollection;
    }

    public async Task<Result> Handle(DeleteExamCommand request, CancellationToken cancellationToken)
    {
        var userId = _userServices.GetCurrentUserId();
        var exam = await _context.Exams
            .Where(x => x.Id == request.ExamId)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

        if (exam is null)
            return ExamErrors.IdNotFound(request.ExamId);

        var room = await _context.Rooms
            .Where(x => x.Id == exam.RoomId)
            .Select(x => new { x.DoctorId })
            .FirstOrDefaultAsync(cancellationToken);

        if (room.DoctorId != userId)
            return ExamErrors.UnAuthorizeAction;

        await _context.ExamAnswers
            .Where(x => x.ExamId == request.ExamId)
            .ExecuteDeleteAsync(cancellationToken);

        var result = await _context.Exams
            .Where(x => x.Id == request.ExamId)
            .ExecuteDeleteAsync(cancellationToken);

        if (result == 0)
            return Error.UnKnown;
        _eventCollection.Raise(new ExamDeletedEvent(exam));
        return Result.Success();
    }
}