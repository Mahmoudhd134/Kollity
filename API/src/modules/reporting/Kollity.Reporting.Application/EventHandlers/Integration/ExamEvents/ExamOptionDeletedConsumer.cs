using Kollity.Exams.Contracts.Exam;
using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Application.Exceptions;
using Kollity.Reporting.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Reporting.Application.EventHandlers.Integration.ExamEvents;

public class ExamOptionDeletedConsumer(ReportingDbContext context)
    : IntegrationEventConsumer<ExamOptionDeletedIntegrationEvent>
{
    protected override async Task Handle(ExamOptionDeletedIntegrationEvent integrationEvent)
    {
        var option = await context.ExamsQuestionOptions
            .FirstOrDefaultAsync(x => x.Id == integrationEvent.Id);
        if (option is null)
            throw new ExamExceptions.ExamQuestionOptionNotFound(integrationEvent.Id);
        if (option.IsRightOption)
            throw new ExamExceptions.CanNotDeleteExamOptionThatIsRightOption(integrationEvent.Id);

        context.ExamsQuestionOptions.Remove(option);
        
        await context.ExamAnswers
            .Where(x => x.ExamQuestionOptionId == option.Id)
            .ExecuteDeleteAsync();
        
        await context.SaveChangesAsync();
    }
}