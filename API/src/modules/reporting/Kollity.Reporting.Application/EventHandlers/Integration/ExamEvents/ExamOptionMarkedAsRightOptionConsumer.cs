using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Application.Exceptions;
using Kollity.Reporting.Persistence.Data;
using Kollity.Services.Contracts.Exam;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Reporting.Application.EventHandlers.Integration.ExamEvents;

public class ExamOptionMarkedAsRightOptionConsumer(ReportingDbContext context)
    : IntegrationEventConsumer<ExamOptionMarkedAsRightOptionIntegrationEvent>
{
    protected override async Task Handle(ExamOptionMarkedAsRightOptionIntegrationEvent integrationEvent)
    {
        var question = await context.ExamQuestions
            .Include(x => x.ExamQuestionOptions)
            .FirstOrDefaultAsync(x => x.Id == integrationEvent.ExamQuestionId);

        if (question is null)
            throw new ExamExceptions.ExamQuestionNotFound(integrationEvent.ExamQuestionId);

        var optionExists = question.ExamQuestionOptions.Any(x => x.Id == integrationEvent.Id);
        if (optionExists == false)
            throw new ExamExceptions.ExamQuestionOptionNotFound(integrationEvent.Id);

        question.ExamQuestionOptions.ForEach(x => x.IsRightOption = false);
        question.ExamQuestionOptions.First(x => x.Id == integrationEvent.Id).IsRightOption = true;

        await context.SaveChangesAsync();
    }
}