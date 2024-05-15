using Kollity.Common.ErrorHandling;

namespace Kollity.Services.Domain.Errors;

public static class CourseErrors
{
    public static readonly Error NonDoctorAssignation = Error.Validation("Course.NonDoctorAssignation",
        "Can not assign a non doctor as a course doctor.");

    public static readonly Error NonAssistantAssignation = Error.Validation("Course.NonAssistantAssignation",
        "Can not assign a non assistant as one of course assistants.");

    public static readonly Error HasAnAssignedDoctor = Error.Validation("Course.HasAnAssignedDoctor",
        "The course already assigned to a doctor.");

    public static readonly Error AssistantAlreadyAssigned = Error.Conflict("AssistantAlreadyAssigned",
        "The assistant is already assigned to this course.");

    public static readonly Error StudentAlreadyAssigned = Error.Conflict("Course.StudentAlreadyAssigned",
        "The student is already assigned to this course.");

    public static readonly Error StudentIsNotAssignedToThisCourse = Error.Conflict(
        "Course.StudentIsNotAssignedToThisCourse",
        "The student is not assigned to this course to de assign him.");

    public static readonly Error UnAuthorizeAddRoom = Error.Validation("UnAuthorizeAddRoom",
        "You can not add room to a course that you are not assigned to");

    public static readonly Error HasNoDoctor = Error.Validation("Course.HasNoDoctor",
        "You can not perform this action on a course that has not assigned doctor.");

    public static Error DuplicatedCode(int code)
    {
        return Error.Conflict("Course.DuplicatedCode",
            $"There is already course with code '{code}'.");
    }

    public static Error IdNotFound(Guid courseId)
    {
        return Error.NotFound("Course.WrongId",
            $"The id '{courseId}' is wrong.");
    }

    public static Error AssistantNotAssigned(Guid assistantId)
    {
        return Error.NotFound("AssistantNotAssigned",
            $"The assistant with id '{assistantId}' is not assigned to this course.");
    }
}