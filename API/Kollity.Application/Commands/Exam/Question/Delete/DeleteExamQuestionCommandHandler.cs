using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Exam.Question.Delete;

public class DeleteExamQuestionCommandHandler : ICommandHandler<DeleteExamQuestionCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserServices _userServices;

    public DeleteExamQuestionCommandHandler(ApplicationDbContext context, IUserServices userServices)
    {
        _context = context;
        _userServices = userServices;
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
        if (exam.DoctorId == null)
            return RoomErrors.RoomHasNoDoctor;
        if (exam.DoctorId != userId)
            return ExamErrors.UnAuthorizeAction;
        
        await _context.ExamQuestions
            .Where(x => x.Id == questionId)
            .ExecuteDeleteAsync(cancellationToken);
        
        return Result.Success();
    }
}