using Kollity.Exams.Contracts.Exam;
using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Application.Exceptions;
using Kollity.Reporting.Domain.ExamModels;
using Kollity.Reporting.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Reporting.Application.EventHandlers.Integration.ExamEvents;

public class ExamAnswerSubmittedConsumer(ReportingDbContext context)
    : IntegrationEventConsumer<ExamAnswerSubmittedIntegrationEvent>
{
    protected override async Task Handle(ExamAnswerSubmittedIntegrationEvent integrationEvent)
    {
        var exam = await context.Exams
            .Where(x => x.Id == integrationEvent.ExamId)
            .Select(x => new { x.RoomId })
            .FirstOrDefaultAsync();
        if (exam is null)
            throw new ExamExceptions.ExamNotFound(integrationEvent.ExamId);

        var optionExists = await context.ExamsQuestionOptions
            .AnyAsync(x => x.Id == integrationEvent.OptionId);
        if (optionExists == false)
            throw new ExamExceptions.ExamQuestionOptionNotFound(integrationEvent.OptionId);

        var answer = new ExamAnswer
        {
            RoomId = exam.RoomId,
            ExamId = integrationEvent.ExamId,
            ExamQuestionId = integrationEvent.QuestionId,
            ExamQuestionOptionId = integrationEvent.OptionId,
            RequestTime = integrationEvent.RequestTime,
            SubmitTime = integrationEvent.SubmitTime,
            StudentId = integrationEvent.UserId
        };

        context.ExamAnswers.Add(answer);
        await context.SaveChangesAsync();
    }
}