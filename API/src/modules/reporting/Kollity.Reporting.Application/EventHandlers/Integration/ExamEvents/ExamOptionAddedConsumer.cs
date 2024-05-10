using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Application.Exceptions;
using Kollity.Reporting.Domain.ExamModels;
using Kollity.Reporting.Persistence.Data;
using Kollity.Services.Contracts.Exam;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Reporting.Application.EventHandlers.Integration.ExamEvents;

public class ExamOptionAddedConsumer(ReportingDbContext context)
    : IntegrationEventConsumer<ExamOptionAddedIntegrationEvent>
{
    protected override async Task Handle(ExamOptionAddedIntegrationEvent integrationEvent)
    {
        var questionExists = await context.ExamQuestions
            .AnyAsync(x => x.Id == integrationEvent.ExamQuestionId);
        if (questionExists == false)
            throw new ExamExceptions.ExamQuestionNotFound(integrationEvent.ExamQuestionId);
        var option = new ExamQuestionOption()
        {
            Id = integrationEvent.Id,
            ExamQuestionId = integrationEvent.ExamQuestionId,
            IsRightOption = integrationEvent.IsRightOption,
            Option = integrationEvent.Option
        };
        context.ExamsQuestionOptions.Add(option);
        await context.SaveChangesAsync();
    }
}