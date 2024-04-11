using Kollity.Services.Domain.ExamModels;
using Kollity.Services.Application.Dtos.Exam;
using Kollity.Services.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Exam.Question.Add;

public class AddExamQuestionCommandHandler : ICommandHandler<AddExamQuestionCommand, ExamQuestionDto>
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

    public async Task<Result<ExamQuestionDto>> Handle(AddExamQuestionCommand request,
        CancellationToken cancellationToken)
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
                x.EndDate
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
        question.ExamQuestionOptions.Add(new ExamQuestionOption()
        {
            Option =
                "Temporally option, make another option, mark it as the right one, then remove this option.\nThis is for ensuring that at leaset one option will be present.",
            IsRightOption = true,
        });
        _context.ExamQuestions.Add(question);
        var result = await _context.SaveChangesAsync(cancellationToken);
        if (result == 0)
            return Error.UnKnown;

        return _mapper.Map<ExamQuestionDto>(question);
    }
}