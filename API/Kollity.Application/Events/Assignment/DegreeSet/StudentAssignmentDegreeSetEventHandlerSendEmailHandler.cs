using Kollity.Application.Dtos.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Events.Assignment.DegreeSet;

public class StudentAssignmentDegreeSetEventHandlerSendEmailHandler : IEventHandler<StudentAssignmentDegreeSetEvent>
{
    private readonly ApplicationDbContext _context;
    private readonly IEmailService _emailService;

    public StudentAssignmentDegreeSetEventHandlerSendEmailHandler(ApplicationDbContext context,
        IEmailService emailService)
    {
        _context = context;
        _emailService = emailService;
    }

    public async Task Handle(StudentAssignmentDegreeSetEvent notification, CancellationToken cancellationToken)
    {
        var (assignmentId, studentId, degree, degreeSetOn) = notification;

        var userEmail = await _context.Students
            .Where(x => x.EmailConfirmed && x.EnabledEmailNotifications)
            .Where(x => x.Id == studentId)
            .Select(x => new UserEmailDto
            {
                Email = x.Email,
                FullName = x.FullNameInArabic
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (userEmail is null)
            return;

        var name = await _context.Assignments
            .Where(x => x.Id == assignmentId)
            .Select(x => x.Name)
            .FirstOrDefaultAsync(cancellationToken);

        if (string.IsNullOrWhiteSpace(name))
            return;

        await _emailService.TrySendAssignmentDegreeSetEmailAsync(userEmail, name, degree, degreeSetOn);
    }
}