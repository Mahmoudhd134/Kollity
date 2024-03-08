using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Kollity.Domain.StudentModels;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Course.AssignStudent;

public class AssignStudentToCourseCommandHandler : ICommandHandler<AssignStudentToCourseCommand>
{
    private readonly ApplicationDbContext _context;

    public AssignStudentToCourseCommandHandler(ApplicationDbContext context)
    {
        _context = context;
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

        _context.StudentCourses.Add(new StudentCourse
        {
            StudentId = studentId,
            CourseId = courseId
        });

        var result = await _context.SaveChangesAsync(cancellationToken);
        return result > 0 ? Result.Success() : Error.UnKnown;
    }
}