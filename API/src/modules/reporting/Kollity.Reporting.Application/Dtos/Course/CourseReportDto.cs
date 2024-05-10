using Kollity.Reporting.Domain.UserModels;

namespace Kollity.Reporting.Application.Dtos.Course;

public class CourseReportDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Code { get; set; }
    public string Department { get; set; }
    public int Hours { get; set; }
    public List<DoctorForCourseReportDto> Doctors { get; set; }
    public List<RoomForCourseReportDto> Rooms { get; set; }
}

public class DoctorForCourseReportDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Image { get; set; }
    public DateTime AssignedAtUtc { get; set; }
    public DoctorType DoctorType { get; set; }
    public bool IsCurrentlyAssigned { get; set; }
}

public class RoomForCourseReportDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid DoctorId { get; set; }
    public string DoctorName { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public ExamsForCourseReportDto Exams { get; set; }
    public AssignmentsForCourseReportDto Assignments { get; set; }
}

public class ExamsForCourseReportDto
{
    public int NumberOfExams { get; set; }
    public double AverageExamQuestionCount { get; set; }
    public double AverageDegreeForEachExam { get; set; }
    public double AverageAverageDegreesForEachStudent { get; set; }
    public double MaxAverageStudentDegrees { get; set; }
    public double MinAverageStudentDegrees { get; set; }
}

public class AssignmentsForCourseReportDto
{
    public int NumberOfAssignments { get; set; }
    public double AverageDegreeForEachAssignment { get; set; }
    public double AverageAverageDegreesForEachStudent { get; set; }
    public double MaxAverageStudentDegrees { get; set; }
    public double MinAverageStudentDegrees { get; set; }
}