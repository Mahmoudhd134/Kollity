using Kollity.Common.ErrorHandling;
using Kollity.Reporting.Application.Dtos.Course;
using Kollity.Reporting.Application.Extensions;
using Kollity.Reporting.Application.Queries.Common;
using Kollity.Reporting.Domain.Errors;
using Kollity.Reporting.Domain.UserModels;
using Kollity.Reporting.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Reporting.Application.Queries.Course;

public class GetCourseReportQueryHandler(ReportingDbContext context)
    : IQueryHandler<GetCourseReportQuery, CourseReportDto>
{
    public async Task<Result<CourseReportDto>> Handle(GetCourseReportQuery request, CancellationToken cancellationToken)
    {
        DateTime? from = request.From?.ToUniversalTime(),
            to = request.To?.ToUniversalTime();

        var courseDto = await context.Courses
            .Where(x => x.Id == request.Id)
            .Select(x => new CourseReportDto
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                Department = x.Department,
                Hours = x.Hours,
                Doctors = x.CourseDoctorAndAssistants
                    .Where(cd => from == null || cd.AssigningDate >= from)
                    .Where(cd => to == null || cd.AssigningDate <= to)
                    .Select(d => new DoctorForCourseReportDto
                    {
                        Id = d.Id,
                        Image = d.Doctor.ProfileImage,
                        FullName = d.Doctor.FullNameInArabic,
                        DoctorType = d.IsDoctor ? DoctorType.Doctor : DoctorType.Assistant,
                        IsCurrentlyAssigned = d.IsCurrentlyAssigned,
                        AssignedAtUtc = d.AssigningDate
                    })
                    .ToList(),
                Rooms = x.Rooms
                    .Where(r => from == null || r.CreatedAt >= from)
                    .Where(r => to == null || r.CreatedAt <= to)
                    .Select(r => new RoomForCourseReportDto
                    {
                        Id = r.Id,
                        Name = r.Name,
                        DoctorId = r.DoctorId,
                        DoctorName = r.Doctor.FullNameInArabic,
                        CreatedAtUtc = r.CreatedAt
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (courseDto is null)
            return CourseErrors.IdNotFound(request.Id);

        var rowExams = await context.Rooms
            .Where(x => x.CourseId == request.Id)
            .Where(r => from == null || r.CreatedAt >= from)
            .Where(r => to == null || r.CreatedAt <= to)
            .Select(r => new
            {
                r.Id,
                Exams = new ExamQueryResult()
                {
                    NumberOfExams = r.Exams.Count,
                    CountOfAllQuestions = r.Exams.SelectMany(e => e.ExamQuestions).Count(),
                    AvgQuestionDegree = r.Exams.SelectMany(e => e.ExamQuestions).Select(q => (int?)q.Degree).Average(),
                    SumOfAllDegrees = r.ExamAnswers
                        .Select(a => a.ExamQuestionOption.IsRightOption ? a.ExamQuestion.Degree : (int?)0)
                        .Sum(),
                    MaxSumOfStudentDegree = r.ExamAnswers
                        .GroupBy(a => a.StudentId)
                        .Select(g => g
                            .Select(sa => sa.ExamQuestionOption.IsRightOption ? sa.ExamQuestion.Degree : (int?)0)
                            .Sum())
                        .Max(),
                    MinSumOfStudentDegree = r.ExamAnswers
                        .GroupBy(a => a.StudentId)
                        .Select(g => g
                            .Select(sa => sa.ExamQuestionOption.IsRightOption ? sa.ExamQuestion.Degree : (int?)0)
                            .Sum())
                        .Min(),
                    NumberOfStudentsAnswers = r.ExamAnswers.Select(a => a.StudentId).Distinct().Count()
                }
            })
            .ToDictionaryAsync(x => x.Id, cancellationToken);

        var rowAssignments = await context.Rooms
            .Where(x => x.CourseId == request.Id)
            .Where(r => from == null || r.CreatedAt >= from)
            .Where(r => to == null || r.CreatedAt <= to)
            .Select(r => new
            {
                r.Id,
                Assignments = new AssignmentQueryResult
                {
                    NumberOfAssignments = r.Assignments.Count,
                    AvgAssignmentsDegree = r.Assignments.Select(a => (int?)a.Degree).Average(),
                    SumOfAllDegrees = r.AssignmentAnswers
                        .Select(a => (int?)a.Degree)
                        .Sum(),
                    MaxSumStudentDegree = r.AssignmentAnswers
                        .GroupBy(a => a.StudentId)
                        .Select(g => g.Select(a => (int?)a.Degree).Sum())
                        .Max(),
                    MinSumStudentDegree = r.AssignmentAnswers
                        .GroupBy(a => a.StudentId)
                        .Select(g => g.Select(a => (int?)a.Degree).Sum())
                        .Min(),
                    NumberOfStudentsAnswers = r.AssignmentAnswers.Select(a => a.StudentId).Distinct().Count()
                }
            })
            .ToDictionaryAsync(x => x.Id, cancellationToken);


        courseDto.Rooms.ForEach(r =>
        {
            var result = rowExams.TryGetValue(r.Id, out var exams);
            if (result)
                r.Exams = new ExamsForCourseReportDto
                {
                    NumberOfExams = exams.Exams.NumberOfExams,

                    AverageDegreeForEachExam = (exams.Exams.AvgQuestionDegree ?? 0D) *
                                               ((double)exams.Exams.CountOfAllQuestions /
                                                exams.Exams.NumberOfExams.ReplaceIfZero(1)),

                    AverageAverageDegreesForEachStudent =
                        (exams.Exams.SumOfAllDegrees ?? 0D) / exams.Exams.NumberOfStudentsAnswers.ReplaceIfZero(1) /
                        exams.Exams.CountOfAllQuestions.ReplaceIfZero(1) *
                        ((double)exams.Exams.CountOfAllQuestions / exams.Exams.NumberOfExams.ReplaceIfZero(1)),
                    AverageExamQuestionCount =
                        (double)exams.Exams.CountOfAllQuestions / exams.Exams.NumberOfExams.ReplaceIfZero(1),

                    MinAverageStudentDegrees = (exams.Exams.MinSumOfStudentDegree ?? 0D) /
                                               exams.Exams.CountOfAllQuestions.ReplaceIfZero(1) *
                                               ((double)exams.Exams.CountOfAllQuestions /
                                                exams.Exams.NumberOfExams.ReplaceIfZero(1)),

                    MaxAverageStudentDegrees = (exams.Exams.MaxSumOfStudentDegree ?? 0D) /
                                               exams.Exams.CountOfAllQuestions.ReplaceIfZero(1) *
                                               ((double)exams.Exams.CountOfAllQuestions /
                                                exams.Exams.NumberOfExams.ReplaceIfZero(1))
                };

            result = rowAssignments.TryGetValue(r.Id, out var assignments);
            if (result)
                r.Assignments = new AssignmentsForCourseReportDto
                {
                    NumberOfAssignments = assignments.Assignments.NumberOfAssignments,

                    AverageDegreeForEachAssignment = assignments.Assignments.AvgAssignmentsDegree ?? 0,

                    AverageAverageDegreesForEachStudent = (assignments.Assignments.SumOfAllDegrees ?? 0D) /
                                                          assignments.Assignments.NumberOfStudentsAnswers.ReplaceIfZero(
                                                              1) /
                                                          assignments.Assignments.NumberOfAssignments.ReplaceIfZero(1),

                    MaxAverageStudentDegrees = (assignments.Assignments.MaxSumStudentDegree ?? 0D) /
                                               assignments.Assignments.NumberOfAssignments.ReplaceIfZero(1),

                    MinAverageStudentDegrees = (assignments.Assignments.MinSumStudentDegree ?? 0D) /
                                               assignments.Assignments.NumberOfAssignments.ReplaceIfZero(1)
                };
        });

        return courseDto;
    }
}