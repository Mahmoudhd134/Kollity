using Domain.ErrorHandlers;

namespace Domain.CourseModels;

public static class CourseErrors
{
    public static Error DuplicatedCode(int code) => Error.Conflict("Course.DuplicatedCode",
        $"There is already course with code '{code}'.");

    public static Error WrongId(Guid courseId) => Error.NotFound("Course.WrongId",
        $"The id '{courseId}' is wrong.");
}