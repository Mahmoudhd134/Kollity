using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Application.Exceptions;
using Kollity.Reporting.Domain.ExamModels;
using Kollity.Reporting.Persistence.Data;
using Kollity.Services.Contracts.Exam;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Reporting.Application.EventHandlers.Integration.ExamEvents;

public class ExamAddedConsumer(ReportingDbContext context) : IntegrationEventConsumer<ExamAddedIntegrationEvent>
{
    protected override async Task Handle(ExamAddedIntegrationEvent integrationEvent)
    {
        var roomExists = await context.Rooms
            .AnyAsync(x => x.Id == integrationEvent.RoomId);
        if (roomExists == false)
            throw new RoomExceptions.RoomNotFound(integrationEvent.RoomId);
        var exam = new Exam
        {
            Id = integrationEvent.Id,
            Name = integrationEvent.Name,
            StartDate = integrationEvent.StartDate,
            EndDate = integrationEvent.EndDate,
            CreationDate = integrationEvent.CreationDate,
            RoomId = integrationEvent.RoomId
        };
        context.Exams.Add(exam);
        await context.SaveChangesAsync();
    }
}