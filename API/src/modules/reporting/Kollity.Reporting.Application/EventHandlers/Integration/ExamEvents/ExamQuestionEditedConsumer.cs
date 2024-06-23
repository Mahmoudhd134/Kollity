using Kollity.Exams.Contracts.Exam;
using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Application.Exceptions;
using Kollity.Reporting.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Reporting.Application.EventHandlers.Integration.ExamEvents;

public class ExamQuestionEditedConsumer(ReportingDbContext context)
    : IntegrationEventConsumer<ExamQuestionEditedIntegrationEvent>
{
    protected override async Task Handle(ExamQuestionEditedIntegrationEvent integrationEvent)
    {
        var question = await context.ExamQuestions
            .FirstOrDefaultAsync(x => x.Id == integrationEvent.Id);
        if (question is null)
            throw new ExamExceptions.ExamQuestionNotFound(integrationEvent.Id);

        question.Question = integrationEvent.Question;
        question.Degree = integrationEvent.Degree;
        question.OpenForSeconds = integrationEvent.OpenForSeconds;

        await context.SaveChangesAsync();
    }
}