using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Exam.Question.Option.Delete;

public class DeleteExamQuestionOptionCommandHandler : ICommandHandler<DeleteExamQuestionOptionCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserServices _userServices;

    public DeleteExamQuestionOptionCommandHandler(ApplicationDbContext context, IUserServices userServices)
    {
        _context = context;
        _userServices = userServices;
    }

    public async Task<Result> Handle(DeleteExamQuestionOptionCommand request, CancellationToken cancellationToken)
    {
        Guid userId = _userServices.GetCurrentUserId(),
            optionId = request.OptionId;

        var exam = await _context.ExamQuestionOptions
            .Where(x => x.Id == optionId)
            .Select(x => new
            {
                x.ExamQuestion.Exam.Room.DoctorId,
                x.ExamQuestion.Exam.StartDate
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (exam is null)
            return ExamErrors.OptionNotFound(optionId);
        if (exam.DoctorId == null)
            return RoomErrors.RoomHasNoDoctor;
        if (exam.DoctorId != userId)
            return ExamErrors.UnAuthorizeAction;
        if (DateTime.UtcNow >= exam.StartDate)
            return ExamErrors.CanNotEditExamAfterItStarts;

        var isRightOption = await _context.ExamQuestionOptions
            .AnyAsync(x => x.Id == optionId && x.IsRightOption, cancellationToken);

        if (isRightOption)
            return ExamErrors.DeleteRightOption;

        await _context.ExamQuestionOptions
            .Where(x => x.Id == optionId)
            .ExecuteDeleteAsync(cancellationToken);
        
        return Result.Success();
    }
}