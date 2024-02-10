using Kollity.Domain.CourseModels;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Course.Add;

public class AddCourseCommandHandler : ICommandHandler<AddCourseCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AddCourseCommandHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result> Handle(AddCourseCommand request, CancellationToken cancellationToken)
    {
        var addCourseDto = request.AddCourseDto;

        var sameCodeFound = await _context.Courses.AnyAsync(x => x.Code == addCourseDto.Code, cancellationToken);
        if (sameCodeFound)
            return CourseErrors.DuplicatedCode(addCourseDto.Code);

        var course = _mapper.Map<Domain.CourseModels.Course>(addCourseDto);
        _context.Courses.Add(course);
        var result = await _context.SaveChangesAsync(cancellationToken);
        return result > 0 ? Result.Success() : Error.UnKnown;
    }
}