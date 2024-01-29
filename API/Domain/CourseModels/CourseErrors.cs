using Domain.ErrorHandlers;

namespace Domain.CourseModels;

public static class CourseErrors
{
    public static Error DuplicatedCode(int code) => Error.Conflict("Course.DuplicatedCode",
        $"There is already course with code '{code}'.");

    public static Error IdNotFound(Guid courseId) => Error.NotFound("Course.WrongId",
        $"The id '{courseId}' is wrong.");

    public static readonly Error NonDoctorAssignation = Error.Validation("Course.NonDoctorAssignation",
        "Can not assign a non doctor as a course doctor.");

    public static readonly Error NonAssistantAssignation = Error.Validation("Course.NonAssistantAssignation",
        "Can not assign a non assistant as one of course assistants.");

    public static readonly Error HasAnAssignedDoctor = Error.Validation("Course.HasAnAssignedDoctor",
        "The course already assigned to a doctor.");

    public static readonly Error AssistantAlreadyAssigned = Error.Conflict("AssistantAlreadyAssigned",
        "The assistant is already assigned to this course.");

    public static Error AssistantNotAssigned(Guid assistantId) => Error.NotFound("AssistantNotAssigned",
        $"The assistant with id '{assistantId}' is not assigned to this course.");
}