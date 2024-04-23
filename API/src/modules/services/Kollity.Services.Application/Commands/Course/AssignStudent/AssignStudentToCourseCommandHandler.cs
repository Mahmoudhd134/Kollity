using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Courses;
using Kollity.Services.Domain.StudentModels;
using Kollity.Services.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Course.AssignStudent;

public class AssignStudentToCourseCommandHandler : ICommandHandler<AssignStudentToCourseCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly EventCollection _eventCollection;

    public AssignStudentToCourseCommandHandler(ApplicationDbContext context, EventCollection eventCollection)
    {
        _context = context;
        _eventCollection = eventCollection;
    }

    public async Task<Result> Handle(AssignStudentToCourseCommand request, CancellationToken cancellationToken)
    {
        var studentId = request.CourseStudentIdsMap.StudentId;
        var courseId = request.CourseStudentIdsMap.CourseId;

        var student = await _context.Students.AnyAsync(x => x.Id == studentId, cancellationToken);
        if (student == false)
            return StudentErrors.IdNotFound(studentId);

        var course = await _context.Courses.AnyAsync(x => x.Id == courseId, cancellationToken);
        if (course == false)
            return CourseErrors.IdNotFound(courseId);

        var isAssigned = await _context.StudentCourses.AnyAsync(x =>
            x.StudentId == studentId && x.CourseId == courseId, cancellationToken);
        if (isAssigned)
            return CourseErrors.StudentAlreadyAssigned;

        var studentCourse = new StudentCourse
        {
            StudentId = studentId,
            CourseId = courseId
        };
        _context.StudentCourses.Add(studentCourse);

        var result = await _context.SaveChangesAsync(cancellationToken);
        if (result == 0)
            return Error.UnKnown;
        _eventCollection.Raise(new CourseStudentAssignedEvent(studentCourse));
        return Result.Success();
    }
}