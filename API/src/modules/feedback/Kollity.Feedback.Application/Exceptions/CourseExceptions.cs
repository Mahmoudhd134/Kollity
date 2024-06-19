using Kollity.Feedback.Application.Exceptions.Generic;

namespace Kollity.Feedback.Application.Exceptions;

public static class CourseExceptions
{
    public class CourseNotFound(Guid id) : NotFoundException($"Course wit hid {id} not found");

    public class StudentAlreadyAssigned(Guid studentId, Guid courseId)
        : Exception($"Student with id {studentId} is already assigned to course {courseId}");

    public class StudentIsNotAssigned(Guid studentId, Guid courseId)
        : Exception($"Student with id {studentId} is not assigned to course {courseId}");
}