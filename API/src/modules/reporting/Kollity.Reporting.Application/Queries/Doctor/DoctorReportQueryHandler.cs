using Kollity.Common.ErrorHandling;
using Kollity.Reporting.Application.Dtos.Course;
using Kollity.Reporting.Application.Dtos.Doctor;
using Kollity.Reporting.Application.Extensions;
using Kollity.Reporting.Application.Queries.Common;
using Kollity.Reporting.Domain.Errors;
using Kollity.Reporting.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Reporting.Application.Queries.Doctor;

public class DoctorReportQueryHandler(ReportingDbContext context) : IQueryHandler<DoctorReportQuery, DoctorReportDto>
{
    public async Task<Result<DoctorReportDto>> Handle(DoctorReportQuery request, CancellationToken cancellationToken)
    {
        DateTime? from = request.From?.ToUniversalTime(),
            to = request.To?.ToUniversalTime();
        var doctorDto = await context.Doctors
            .Where(x => x.Id == request.Id)
            .Select(d => new DoctorReportDto
            {
                Id = d.Id,
                FullName = d.FullNameInArabic,
                UserName = d.UserName,
                Image = d.ProfileImage,
                Courses = d.CourseDoctorAndAssistants
                    .Where(cd => from == null || cd.AssigningDate >= from)
                    .Where(cd => to == null || cd.AssigningDate <= to)
                    .Select(cd => new CourseMetaForDoctorReportDto
                    {
                        Id = cd.CourseId,
                        Name = cd.Course.Name,
                        Code = cd.Course.Code,
                        Department = cd.Course.Department,
                        AssignedDate = cd.AssigningDate,
                        IsDoctor = cd.IsDoctor,
                        IsCurrentlyAssigned = cd.IsCurrentlyAssigned
                    })
                    .ToList(),
                Rooms = d.Rooms
                    .Where(r => from == null || r.CreatedAt >= from)
                    .Where(r => to == null || r.CreatedAt <= to)
                    .Select(r => new RoomForDoctorReportDto
                    {
                        Id = r.Id,
                        Name = r.Name,
                        CreatedAtUtc = r.CreatedAt,
                        CourseId = r.CourseId,
                        CourseCode = r.Course.Code,
                        CourseName = r.Course.Name,
                        CourseDepartment = r.Course.Department,
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (doctorDto is null)
            return DoctorErrors.IdNotFound(request.Id);


        var rowExams = await context.Rooms
            .Where(x => x.DoctorId == request.Id)
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
            .Where(x => x.DoctorId == request.Id)
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


        doctorDto.Rooms.ForEach(r =>
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

        return doctorDto;
    }
}