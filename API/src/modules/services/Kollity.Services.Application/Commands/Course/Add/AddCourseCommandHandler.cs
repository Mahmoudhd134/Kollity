using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Courses;
using Kollity.Services.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Course.Add;

public class AddCourseCommandHandler : ICommandHandler<AddCourseCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly EventCollection _eventCollection;

    public AddCourseCommandHandler(ApplicationDbContext context, IMapper mapper, EventCollection eventCollection)
    {
        _context = context;
        _mapper = mapper;
        _eventCollection = eventCollection;
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
        if (result == 0)
            return Error.UnKnown;
        _eventCollection.Raise(new CourseAddedEvent(course));
        return Result.Success();
    }
}