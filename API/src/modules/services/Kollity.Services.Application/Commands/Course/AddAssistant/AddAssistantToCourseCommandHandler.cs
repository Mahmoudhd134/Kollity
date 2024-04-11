using Kollity.Services.Domain.CourseModels;
using Kollity.Services.Domain.Errors;
using Kollity.Services.Domain.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Course.AddAssistant;

public class AddAssistantToCourseCommandHandler : ICommandHandler<AddAssistantToCourseCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserServices _userServices;

    public AddAssistantToCourseCommandHandler(ApplicationDbContext context, IUserServices userServices)
    {
        _context = context;
        _userServices = userServices;
    }

    public async Task<Result> Handle(AddAssistantToCourseCommand request, CancellationToken cancellationToken)
    {
        var assistantId = request.CourseDoctorIdsMap.DoctorId;
        var courseId = request.CourseDoctorIdsMap.CourseId;

        var doctor = await _context.Doctors.FirstOrDefaultAsync(x => x.Id == assistantId, cancellationToken);
        if (doctor is null)
            return DoctorErrors.IdNotFound(assistantId);

        var course = await _context.Courses
            .FirstOrDefaultAsync(x => x.Id == courseId, cancellationToken);
        if (course is null)
            return CourseErrors.IdNotFound(courseId);

        var isAssigned = await _context.CourseAssistants
            .AnyAsync(x => x.AssistantId == assistantId && x.CourseId == courseId,
                cancellationToken);
        if (isAssigned)
            return CourseErrors.AssistantAlreadyAssigned;

        var isInAssistantRole = _userServices.IsInRole(Role.Assistant);
        if (isInAssistantRole == false)
            return CourseErrors.NonAssistantAssignation;

        course.CoursesAssistants.Add(new CourseAssistant
        {
            AssistantId = assistantId,
            CourseId = courseId
        });

        var result = await _context.SaveChangesAsync(cancellationToken);

        return result > 0 ? Result.Success() : Error.UnKnown;
    }
}