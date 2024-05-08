using Kollity.Reporting.Application.Exceptions.Generic;

namespace Kollity.Reporting.Application.Exceptions;

public static class ExamExceptions
{
    public class ExamNotFound(Guid examId) : NotFoundException($"Exam with id {examId} not found");

    public class ExamQuestionNotFound(Guid questionId) : NotFoundException($"Exam question with id {questionId} not found");

    public class ExamQuestionOptionNotFound(Guid optionId)
        : NotFoundException($"Exam question option with id {optionId} not found");

    public class CanNotDeleteExamOptionThatIsRightOption(Guid optionId) : Exception($"Can not remove option {optionId} because it is a right option");
}