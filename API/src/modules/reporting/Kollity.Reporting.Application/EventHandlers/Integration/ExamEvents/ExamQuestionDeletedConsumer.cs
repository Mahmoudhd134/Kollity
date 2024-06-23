using Kollity.Exams.Contracts.Exam;
using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Application.Exceptions;
using Kollity.Reporting.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Reporting.Application.EventHandlers.Integration.ExamEvents;

public class ExamQuestionDeletedConsumer(ReportingDbContext context)
    : IntegrationEventConsumer<ExamQuestionDeletedIntegrationEvent>
{
    protected override async Task Handle(ExamQuestionDeletedIntegrationEvent integrationEvent)
    {
        var question = await context.ExamQuestions
            .FirstOrDefaultAsync(x => x.Id == integrationEvent.Id);
        if (question is null)
            throw new ExamExceptions.ExamQuestionNotFound(integrationEvent.Id);

        context.ExamQuestions.Remove(question);

        await context.ExamAnswers
            .Where(x => x.ExamQuestionId == question.Id)
            .ExecuteDeleteAsync();

        await context.ExamsQuestionOptions
            .Where(x => x.ExamQuestionId == question.Id)
            .ExecuteDeleteAsync();

        await context.SaveChangesAsync();
    }
}