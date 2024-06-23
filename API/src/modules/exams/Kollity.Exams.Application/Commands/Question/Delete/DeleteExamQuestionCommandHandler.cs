using Kollity.Exams.Application.Abstractions.Events;
using Kollity.Exams.Application.Abstractions.Services;
using Kollity.Exams.Application.Events.ExamEvents;
using Kollity.Exams.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Exams.Application.Commands.Question.Delete;

public class DeleteExamQuestionCommandHandler : ICommandHandler<DeleteExamQuestionCommand>
{
    private readonly ExamsDbContext _context;
    private readonly IUserServices _userServices;
    private readonly EventCollection _eventCollection;

    public DeleteExamQuestionCommandHandler(ExamsDbContext context, IUserServices userServices,
        EventCollection eventCollection)
    {
        _context = context;
        _userServices = userServices;
        _eventCollection = eventCollection;
    }

    public async Task<Result> Handle(DeleteExamQuestionCommand request, CancellationToken cancellationToken)
    {
        Guid userId = _userServices.GetCurrentUserId(),
            questionId = request.QuestionId;

        var exam = await _context.ExamQuestions
            .Where(x => x.Id == questionId)
            .Select(x => new
            {
                x.Exam.Room.DoctorId
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (exam is null)
            return ExamErrors.QuestionNotFound(questionId);
        if (exam.DoctorId != userId)
            return ExamErrors.UnAuthorizeAction;

        var question = await _context.ExamQuestions
            .Where(x => x.Id == questionId)
            .FirstOrDefaultAsync(cancellationToken);

        _context.ExamQuestions.Remove(question);
        await _context.SaveChangesAsync(cancellationToken);
        
        _eventCollection.Raise(new ExamQuestionDeletedEvent(question));

        return Result.Success();
    }
}