using Kollity.Contracts.Events.Assignment;
using Kollity.NotificationServices.Abstraction;

namespace Kollity.NotificationServices.EventHandlers.Assignment;

public class StudentAssignmentDegreeSetEventHandler : IEventHandler<StudentAssignmentDegreeSetEvent>
{
    private readonly IEmailService _emailService;

    public StudentAssignmentDegreeSetEventHandler(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public Task Handle(StudentAssignmentDegreeSetEvent notification, CancellationToken cancellationToken)
    {
        var (_, _, assignmentName, student, degree, degreeSetOn) = notification;

        if (student is null)
            return Task.CompletedTask;

        return _emailService.TrySendAssignmentDegreeSetEmailAsync(student, assignmentName, degree, degreeSetOn);
    }
}