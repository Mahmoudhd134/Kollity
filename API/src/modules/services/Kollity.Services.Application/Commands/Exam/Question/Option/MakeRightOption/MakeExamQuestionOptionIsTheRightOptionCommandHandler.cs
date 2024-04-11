using Kollity.Services.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Exam.Question.Option.MakeRightOption;

public class MakeExamQuestionOptionIsTheRightOptionCommandHandler :
    ICommandHandler<MakeExamQuestionOptionIsTheRightOptionCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserServices _userServices;

    public MakeExamQuestionOptionIsTheRightOptionCommandHandler(ApplicationDbContext context,
        IUserServices userServices)
    {
        _context = context;
        _userServices = userServices;
    }

    public async Task<Result> Handle(MakeExamQuestionOptionIsTheRightOptionCommand request,
        CancellationToken cancellationToken)
    {
        Guid userId = _userServices.GetCurrentUserId(),
            optionId = request.OptionId;

        var exam = await _context.ExamQuestionOptions
            .Where(x => x.Id == optionId)
            .Select(x => new
            {
                x.ExamQuestion.Exam.Room.DoctorId,
                x.ExamQuestionId
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (exam is null)
            return ExamErrors.OptionNotFound(optionId);
        if (exam.DoctorId == null)
            return RoomErrors.RoomHasNoDoctor;
        if (exam.DoctorId != userId)
            return ExamErrors.UnAuthorizeAction;


        // mark all option as not the right options
        await _context.ExamQuestionOptions
            .Where(x => x.ExamQuestionId == exam.ExamQuestionId)
            .ExecuteUpdateAsync(c =>
                c.SetProperty(x => x.IsRightOption, false), cancellationToken);
        
        // mark the option as the right one
        await _context.ExamQuestionOptions
            .Where(x => x.Id == optionId)
            .ExecuteUpdateAsync(c =>
                c.SetProperty(x => x.IsRightOption, true), cancellationToken);
        
        return Result.Success();
    }
}