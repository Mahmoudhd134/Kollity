using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Exam.Edit;

public class EditExamCommandHandler : ICommandHandler<EditExamCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserServices _userServices;
    private readonly IMapper _mapper;

    public EditExamCommandHandler(ApplicationDbContext context, IUserServices userServices, IMapper mapper)
    {
        _context = context;
        _userServices = userServices;
        _mapper = mapper;
    }

    public async Task<Result> Handle(EditExamCommand request, CancellationToken cancellationToken)
    {
        Guid examId = request.Dto.Id,
            userId = _userServices.GetCurrentUserId();

        var exam = await _context.Exams
            .Where(x => x.Id == examId)
            .FirstOrDefaultAsync(cancellationToken);

        if (exam is null)
            return ExamErrors.IdNotFound(examId);

        var room = await _context.Rooms
            .Where(x => x.Id == exam.RoomId)
            .Select(x => new { x.DoctorId })
            .FirstOrDefaultAsync(cancellationToken);

        if (room.DoctorId is null)
            return RoomErrors.RoomHasNoDoctor;

        if (userId != room.DoctorId)
            return ExamErrors.UnAuthorizeAction;

        if (DateTime.UtcNow >= exam.StartDate && DateTime.UtcNow <= exam.EndDate)
            return ExamErrors.CanNotEditExamWhileItsOpen;

        if (request.Dto.StartDate >= request.Dto.EndDate)
            return ExamErrors.StartDateCanNotBeAfterEndDate;

        _mapper.Map(request.Dto, exam);
        exam.LastUpdatedDate = DateTime.UtcNow;
        var result = await _context.SaveChangesAsync(cancellationToken);
        return result > 0 ? Result.Success() : Error.UnKnown;
    }
}