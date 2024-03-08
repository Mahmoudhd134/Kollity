using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Exam.Delete;

public class DeleteExamCommandHandler : ICommandHandler<DeleteExamCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserServices _userServices;

    public DeleteExamCommandHandler(ApplicationDbContext context, IUserServices userServices)
    {
        _context = context;
        _userServices = userServices;
    }

    public async Task<Result> Handle(DeleteExamCommand request, CancellationToken cancellationToken)
    {
        var examRoomId = await _context.Exams
            .Where(x => x.Id == request.ExamId)
            .Select(x => x.RoomId)
            .FirstOrDefaultAsync(cancellationToken);

        if (examRoomId == Guid.Empty)
            return ExamErrors.IdNotFound(request.ExamId);

        var room = await _context.Rooms
            .Where(x => x.Id == examRoomId)
            .Select(x => new { x.DoctorId })
            .FirstOrDefaultAsync(cancellationToken);

        if (room.DoctorId is null)
            return RoomErrors.RoomHasNoDoctor;

        await _context.ExamAnswers
            .Where(x => x.ExamId == request.ExamId)
            .ExecuteDeleteAsync(cancellationToken);

        var result = await _context.Exams
            .Where(x => x.Id == request.ExamId)
            .ExecuteDeleteAsync(cancellationToken);

        return result > 0 ? Result.Success() : Error.UnKnown;
    }
}