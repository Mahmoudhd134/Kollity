﻿using Kollity.Exams.Application.Abstractions.Events;
using Kollity.Exams.Application.Abstractions.Services;
using Kollity.Exams.Application.Events.ExamEvents;
using Kollity.Exams.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Exams.Application.Commands.Question.Option.Delete;

public class DeleteExamQuestionOptionCommandHandler : ICommandHandler<DeleteExamQuestionOptionCommand>
{
    private readonly ExamsDbContext _context;
    private readonly IUserServices _userServices;
    private readonly EventCollection _eventCollection;

    public DeleteExamQuestionOptionCommandHandler(ExamsDbContext context, IUserServices userServices,EventCollection eventCollection)
    {
        _context = context;
        _userServices = userServices;
        _eventCollection = eventCollection;
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
        if (exam.DoctorId != userId)
            return ExamErrors.UnAuthorizeAction;
        if (DateTime.UtcNow >= exam.StartDate)
            return ExamErrors.CanNotEditExamAfterItStarts;

        var isRightOption = await _context.ExamQuestionOptions
            .AnyAsync(x => x.Id == optionId && x.IsRightOption, cancellationToken);

        if (isRightOption)
            return ExamErrors.DeleteRightOption;

        await _context.ExamAnswers
            .Where(x => x.ExamQuestionOptionId == optionId)
            .ExecuteDeleteAsync(cancellationToken);

        var option = await _context.ExamQuestionOptions
            .Where(x => x.Id == optionId)
            .FirstOrDefaultAsync(cancellationToken);

        _context.ExamQuestionOptions.Remove(option);
        await _context.SaveChangesAsync(cancellationToken);
        
        _eventCollection.Raise(new ExamQuestionOptionDeletedEvent(option));
        
        return Result.Success();
    }
}