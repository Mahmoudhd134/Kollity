using Kollity.Services.Domain.ErrorHandlers.Abstractions;
using Kollity.Services.Domain.ErrorHandlers.Errors;
using Kollity.Services.Application.Abstractions.Messages;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Course.Edit;

public class EditCourseCommandHandler : ICommandHandler<EditCourseCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public EditCourseCommandHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result> Handle(EditCourseCommand request, CancellationToken cancellationToken)
    {
        var editDto = request.EditCourseDto;

        var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == editDto.Id, cancellationToken);
        if (course is null)
            return CourseErrors.IdNotFound(editDto.Id);

        if (course.Code != editDto.Code)
        {
            var sameCodeFound = await _context.Courses.AnyAsync(x => x.Code == editDto.Code, cancellationToken);
            if (sameCodeFound)
                return CourseErrors.DuplicatedCode(editDto.Code);
        }

        _mapper.Map(editDto, course);

        var result = await _context.SaveChangesAsync(cancellationToken);

        return result > 0 ? Result.Success() : Error.UnKnown;
    }
}