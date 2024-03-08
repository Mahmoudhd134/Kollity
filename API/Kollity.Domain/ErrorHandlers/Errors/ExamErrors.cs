using Kollity.Domain.ErrorHandlers.Abstractions;

namespace Kollity.Domain.ErrorHandlers.Errors;

public static class ExamErrors
{
    public static readonly Error UnAuthorizeAction = Error.Validation("Exam.UnAuthorizeAction",
        "This action can not be performed with any one except the room doctor");

    public static readonly Error StartDateCanNotBeAfterEndDate = Error.Validation("Exam.StartDateCanNotBeAfterEndDate",
        "The start date of an exam can not be after its end date");

    public static readonly Error CanNotEditExamWhileItsOpen = Error.Validation("Exam.CanNotEditExamWhileItsOpen",
        "You can not edit an exam if the date is more than or equal the start date and less than the end date");

    public static Error IdNotFound(Guid examId)
    {
        return Error.NotFound("Exam.IdNotFound", $"There is no exam with id '{examId}'");
    }
}