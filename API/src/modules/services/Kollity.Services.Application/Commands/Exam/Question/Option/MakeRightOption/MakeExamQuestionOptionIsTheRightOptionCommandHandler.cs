using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Exam;
using Kollity.Services.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Exam.Question.Option.MakeRightOption;

public class MakeExamQuestionOptionIsTheRightOptionCommandHandler :
    ICommandHandler<MakeExamQuestionOptionIsTheRightOptionCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserServices _userServices;
    private readonly EventCollection _eventCollection;

    public MakeExamQuestionOptionIsTheRightOptionCommandHandler(ApplicationDbContext context,
        IUserServices userServices, EventCollection eventCollection)
    {
        _context = context;
        _userServices = userServices;
        _eventCollection = eventCollection;
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
        var option = await _context.ExamQuestionOptions
            .Where(x => x.Id == optionId)
            .FirstOrDefaultAsync(cancellationToken);
        option.IsRightOption = true;
        await _context.SaveChangesAsync(cancellationToken);

        _eventCollection.Raise(new ExamQuestionOptionMarkedAsRightOptionEvent(option));
        return Result.Success();
    }
}