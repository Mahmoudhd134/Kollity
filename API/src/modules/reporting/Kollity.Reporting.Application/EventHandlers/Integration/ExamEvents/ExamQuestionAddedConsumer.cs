using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Application.Exceptions;
using Kollity.Reporting.Domain.ExamModels;
using Kollity.Reporting.Persistence.Data;
using Kollity.Services.Contracts.Exam;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Reporting.Application.EventHandlers.Integration.ExamEvents;

public class ExamQuestionAddedConsumer(ReportingDbContext context)
    : IntegrationEventConsumer<ExamQuestionAddedIntegrationEvent>
{
    protected override async Task Handle(ExamQuestionAddedIntegrationEvent integrationEvent)
    {
        var examExists = await context.Exams
            .AnyAsync(x => x.Id == integrationEvent.ExamId);
        if (examExists == false)
            throw new ExamExceptions.ExamNotFound(integrationEvent.ExamId);
        var question = new ExamQuestion
        {
            Id = integrationEvent.Id,
            ExamId = integrationEvent.ExamId,
            Question = integrationEvent.Question,
            OpenForSeconds = integrationEvent.OpenForSeconds,
            Degree = integrationEvent.Degree,
            ExamQuestionOptions =
            [
                new ExamQuestionOption
                {
                    Id = integrationEvent.DefaultOptionId,
                    Option = "default option",
                    IsRightOption = true
                }
            ]
        };
        context.ExamQuestions.Add(question);
        await context.SaveChangesAsync();
    }
}