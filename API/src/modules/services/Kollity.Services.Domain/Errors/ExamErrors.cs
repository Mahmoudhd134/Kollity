using Kollity.Common.ErrorHandling;

namespace Kollity.Services.Domain.Errors;

public static class ExamErrors
{
    public static readonly Error UnAuthorizeAction = Error.Validation("Exam.UnAuthorizeAction",
        "This action can not be performed with any one except the room doctor");

    public static readonly Error StartDateCanNotBeAfterEndDate = Error.Validation("Exam.StartDateCanNotBeAfterEndDate",
        "The start date of an exam can not be after its end date");

    public static readonly Error CanNotEditExamAfterItStarts = Error.Validation("Exam.CanNotEditExamWhileItsOpen",
        "You can not edit an exam if the date is more than or equal the start date");

    public static readonly Error DegreeOutOfRange = Error.Validation("Exam.DegreeOutOfRange",
        "The exam question degree must be a positive number more than zero");

    public static readonly Error QuestionMustAtLeast45Second = Error.Validation("Exam.QuestionMustAtLeast45Second",
        "The least amount of time for any question can not be less than 45 seconds");

    public static readonly Error HasRightOption = Error.Conflict("Exam.HasRightOption",
        "This question already has a right option");

    public static readonly Error DeleteRightOption = Error.Validation("Exam.DeleteRightOption",
        "This option is the right one and you can not delete it");

    public static readonly Error HasNoRightOption = Error.Conflict("Exam.HasNoRightOption",
        "Question has no write option. if this the first option you add and its not the right one mark it the right and change it later");

    public static readonly Error ExamDoseNotStart = Error.Validation("Exam.ExamDoseNotStart",
        "The exam dose not start yet");

    public static readonly Error ExamEnded = Error.Validation("Exam.ExamEnded",
        "The exam ended, so you can not solve any more");

    public static readonly Error NoOtherQuestions = Error.NotFound("Exam.NoOtherQuestions",
        "There are no any non solved question in this exam");

    public static readonly Error AnswerNotFound = Error.NotFound("Exam.AnswerNotFound",
        "There is no answer with the question and you");

    public static readonly Error QuestionAlreadyAnswered = Error.Conflict("Exam.QuestionAlreadyAnswered",
        "You already answered this question before");

    public static readonly Error QuestionTimeEnd = Error.Validation("Exam.QuestionTimeEnd",
        "Time for this question has ended");

    public static readonly Error CanNotReviewBeforeExamEnds = Error.Validation("Exam.CanNotReviewBeforeExamEnds",
        "You can not review an exam if it's open");

    public static readonly Error ExamDoseNotFinishYet = Error.Validation("Exam.ExamDoseNotFinsihYet",
        "You can not perform this operation before the exam ends if it stated");

    public static Error IdNotFound(Guid examId)
    {
        return Error.NotFound("Exam.IdNotFound", $"There is no exam with id '{examId}'");
    }

    public static Error QuestionNotFound(Guid questionId)
    {
        return Error.NotFound("Exam.QuestionNotFound", $"There is no question with id {questionId}");
    }

    public static Error OptionNotFound(Guid optionId)
    {
        return Error.NotFound("Exam.OptionNotFound", $"There is no option with id {optionId}");
    }

    public static Error QuestionHanNoOptionWithId(Guid optionId)
    {
        return Error.NotFound("Exam.QuestionHanNoOptionWithId", $"There is no question with option id {optionId}");
    }
}