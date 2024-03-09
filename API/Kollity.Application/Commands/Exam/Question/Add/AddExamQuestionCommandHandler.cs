using Kollity.Domain.ExamModels;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Exam.Question.Add;

public class AddExamQuestionCommandHandler : ICommandHandler<AddExamQuestionCommand, Guid>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserServices _userServices;
    private readonly IMapper _mapper;

    public AddExamQuestionCommandHandler(ApplicationDbContext context, IUserServices userServices, IMapper mapper)
    {
        _context = context;
        _userServices = userServices;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> Handle(AddExamQuestionCommand request, CancellationToken cancellationToken)
    {
        Guid userId = _userServices.GetCurrentUserId(),
            examId = request.ExamId;

        if (request.Dto.Degree <= 0)
            return ExamErrors.DegreeOutOfRange;

        if (request.Dto.OpenForSeconds < 45)
            return ExamErrors.QuestionMustAtLeast45Second;
        
        var exam = await _context.Exams
            .Where(x => x.Id == examId)
            .Select(x => new
            {
                x.Room.DoctorId,
                x.StartDate,
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (exam is null)
            return ExamErrors.IdNotFound(examId);
        if (exam.DoctorId == null)
            return RoomErrors.RoomHasNoDoctor;
        if (exam.DoctorId != userId)
            return ExamErrors.UnAuthorizeAction;
        if (DateTime.UtcNow >= exam.StartDate)
            return ExamErrors.CanNotEditExamAfterItStarts;
        
        var question = _mapper.Map<ExamQuestion>(request.Dto);
        question.ExamId = examId;
        _context.ExamQuestions.Add(question);
        var result = await _context.SaveChangesAsync(cancellationToken);
        if (result == 0)
            return Error.UnKnown;
        
        return question.Id;
    }
}