using Kollity.Exams.Application.Abstractions.Events;
using Kollity.Exams.Application.Abstractions.Services;
using Kollity.Exams.Application.Events.ExamEvents;
using Kollity.Exams.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Exams.Application.Commands.Edit;

public class EditExamCommandHandler : ICommandHandler<EditExamCommand>
{
    private readonly ExamsDbContext _context;
    private readonly IUserServices _userServices;
    private readonly IMapper _mapper;
    private readonly EventCollection _eventCollection;

    public EditExamCommandHandler(ExamsDbContext context, IUserServices userServices, IMapper mapper,
        EventCollection eventCollection)
    {
        _context = context;
        _userServices = userServices;
        _mapper = mapper;
        _eventCollection = eventCollection;
    }

    public async Task<Result> Handle(EditExamCommand request, CancellationToken cancellationToken)
    {
        Guid examId = request.Dto.Id,
            userId = _userServices.GetCurrentUserId();

        if (request.Dto.StartDate >= request.Dto.EndDate)
            return ExamErrors.StartDateCanNotBeAfterEndDate;

        var exam = await _context.Exams
            .Where(x => x.Id == examId)
            .FirstOrDefaultAsync(cancellationToken);

        if (exam is null)
            return ExamErrors.IdNotFound(examId);

        var utcNow = DateTime.UtcNow;
        if (utcNow > exam.StartDate && utcNow < exam.EndDate)
            return ExamErrors.ExamDoseNotFinishYet;

        var room = await _context.Rooms
            .Where(x => x.Id == exam.RoomId)
            .Select(x => new { x.DoctorId })
            .FirstOrDefaultAsync(cancellationToken);

        if (userId != room.DoctorId)
            return ExamErrors.UnAuthorizeAction;

        _mapper.Map(request.Dto, exam);
        exam.LastUpdatedDate = DateTime.UtcNow;
        var result = await _context.SaveChangesAsync(cancellationToken);
        if (result == 0)
            return Error.UnKnown;
        _eventCollection.Raise(new ExamEditedEvent(exam));
        return Result.Success();
    }
}