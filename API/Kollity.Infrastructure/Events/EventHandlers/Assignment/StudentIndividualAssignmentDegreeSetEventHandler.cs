using System.Diagnostics;
using Kollity.Application.IntegrationEvents.Assignment;
using Kollity.Application.IntegrationEvents.Dto;
using Kollity.Infrastructure.Abstraction;
using Kollity.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Infrastructure.Events.EventHandlers.Assignment;

public class StudentIndividualAssignmentDegreeSetEventHandler : IEventHandler<StudentIndividualAssignmentDegreeSetEvent>
{
    private readonly IEmailService _emailService;
    private readonly ApplicationDbContext _context;

    public StudentIndividualAssignmentDegreeSetEventHandler(IEmailService emailService, ApplicationDbContext context)
    {
        _emailService = emailService;
        _context = context;
    }

    public async Task Handle(StudentIndividualAssignmentDegreeSetEvent notification,
        CancellationToken cancellationToken)
    {
        if (notification.AssignmentAnswer.Degree is null)
            return;
        
        var dto = await _context.AssignmentAnswers
            .Where(x => x.Id == notification.AssignmentAnswer.Id)
            .Where(x => x.Student.EmailConfirmed && x.Student.EnabledEmailNotifications)
            .Select(x => new
            {
                AssignmentName = x.Assignment.Name,
                Student = new UserEmailDto(x.Student.Email, x.Student.FullNameInArabic)
            })
            .FirstOrDefaultAsync(cancellationToken);
        if (dto is null)
            return;

        await _emailService.TrySendAssignmentDegreeSetEmailAsync(
            dto.Student,
            dto.AssignmentName,
            (byte)notification.AssignmentAnswer.Degree);
    }
}