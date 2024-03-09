using Kollity.Contracts.Events.Exam;
using Kollity.EmailServices.Emails;

namespace Kollity.EmailServices.EventHandlers.Exam;

public class ExamAddedEventHandler : IEventHandler<ExamAddedEvent>
{
    private readonly IEmailService _emailService;

    public ExamAddedEventHandler(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public Task Handle(ExamAddedEvent notification, CancellationToken cancellationToken)
    {
        return _emailService.TrySendExamAddedEmailAsync(
            notification.ExamName,
            notification.ExamOpenDate,
            notification.RoomName,
            notification.CourseName,
            notification.StudentsInRoom
        );
    }
}