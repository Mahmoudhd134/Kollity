using Kollity.Feedback.Application.Exceptions.Generic;

namespace Kollity.Feedback.Application.Exceptions;

public static class ExamExceptions
{
    public class ExamNotFound(Guid examId) : NotFoundException($"Exam with id {examId} not found");
}