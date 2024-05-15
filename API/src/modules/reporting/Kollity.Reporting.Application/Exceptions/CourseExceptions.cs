using Kollity.Reporting.Application.Exceptions.Generic;

namespace Kollity.Reporting.Application.Exceptions;

public static class CourseExceptions
{
    public class CourseNotFound(Guid id) : NotFoundException($"Course wit hid {id} not found");

    public class AssistantAlreadyAssigned(Guid assistantId, Guid courseId)
        : Exception($"Assistant with id {assistantId} is already assigned to course {courseId}");

    public class AssistantIsNotAssigned(Guid assistantId, Guid courseId)
        : Exception($"Assistant with id {assistantId} is not assigned to course {courseId}");

    public class AssignDoctorToCourseThatHasADoctor(Guid courseId)
        : Exception($"Course with id {courseId} is already has a doctor, and can not assign another doctor to it");

    public class DoctorIsNotAssigned(Guid doctorId, Guid courseId)
        : Exception($"Doctor with id {doctorId} currently is not assigned to course {courseId}");
    
    public class StudentAlreadyAssigned(Guid studentId, Guid courseId)
        : Exception($"Student with id {studentId} is already assigned to course {courseId}");

    public class StudentIsNotAssigned(Guid studentId, Guid courseId)
        : Exception($"Student with id {studentId} is not assigned to course {courseId}");
}