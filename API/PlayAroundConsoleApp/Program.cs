using Domain.AssignmentModels;
using Domain.AssignmentModels.AssignmentGroupModels;
using Domain.CourseModels;
using Domain.ExamModels;
using Domain.RoomModels;
using Infrastructure.Emails;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Persistence.Data;

var optionsBuilder = new DbContextOptionsBuilder()
    .UseSqlServer("server=.;database=MyCollege;trusted_connection=true;encrypt=false;");
var context = new ApplicationDbContext(optionsBuilder.Options);

// CreateCourse(context);
// CreateAssignment(context);


Console.WriteLine();
return;

void CreateCourse(ApplicationDbContext applicationDbContext)
{
    var entity = new Course
    {
        Name = "course one",
        Department = "cs",
        Code = 123,
        Hours = 3,
        Rooms = [new Room { Name = "room one" }]
    };

    // var course = context.Courses.Include(course => course.Rooms).First();

    var studentTwo = applicationDbContext.Students.First(x => x.NormalizedUserName == "studentTwoAfterEdit".ToUpper());

    var exam = new Exam()
    {
        Name = "exam one",
        CreationDate = DateTime.UtcNow,
        StartDate = DateTime.UtcNow,
        EndDate = DateTime.UtcNow.AddHours(2).AddMinutes(30),
        LastUpdatedDate = DateTime.UtcNow,
        ExamQuestions =
        [
            new ExamQuestion
            {
                Question = "this is questions one", OpenForSeconds = 60,
                ExamQuestionOptions =
                [
                    new ExamQuestionOption { Option = "this is option one", IsRightOption = false },
                    new ExamQuestionOption { Option = "this is option two", IsRightOption = false },
                    new ExamQuestionOption { Option = "all above", IsRightOption = true, }
                ]
            },
            new ExamQuestion
            {
                Question = "this is questions two", OpenForSeconds = 60,
                ExamQuestionOptions =
                [
                    new ExamQuestionOption { Option = "this is option one", IsRightOption = false },
                    new ExamQuestionOption { Option = "this is option two", IsRightOption = true },
                    new ExamQuestionOption { Option = "all above", IsRightOption = false }
                ]
            }
        ]
    };

    entity.Rooms.First().Exams.Add(exam);

    // var exam = context.Exams
    //     .Include(exam => exam.ExamQuestions)
    //     .ThenInclude(examQuestion => examQuestion.ExamQuestionOptions)
    //     .First();

    exam.ExamQuestions.First().ExamQuestionOptions.Skip(2).First().ExamAnswers.Add(new ExamAnswer
        { Student = studentTwo, ExamQuestion = exam.ExamQuestions.First(), Exam = exam });

    applicationDbContext.Add(entity);
    applicationDbContext.SaveChanges();
}

void CreateAssignment(ApplicationDbContext context1)
{
    var course = context1.Courses
        .Include(x => x.Rooms)
        .First();

    var assignment = new Assignment
    {
        Name = "assignment one",
        Description = "this is assignment one",
        DoctorId = course.Rooms.First().DoctorId,
        Mode = AssignmentMode.Individual,
        CreatedDate = DateTime.UtcNow
    };
    var student = context1.Students.First();
    var assignmentGroup = new AssignmentGroup
    {
        Code = 123,
        RoomId = course.Rooms.First().Id,
        AssignmentGroupsStudents =
        [
            new AssignmentGroupStudent
            {
                StudentId = student.Id,
            }
        ]
    };

    course.Rooms.First().Assignments.Add(assignment);
    course.Rooms.First().AssignmentGroups.Add(assignmentGroup);
    context1.SaveChanges();
    context1.AssignmentAnswers.AddRange([
        new AssignmentAnswer
        {
            File = "this is file url",
            AssignmentId = assignment.Id,
            StudentId = student.Id
        },
        new AssignmentAnswer
        {
            File = "this is file url",
            AssignmentId = assignment.Id,
            AssignmentGroupId = assignmentGroup.Id
        }
    ]);

    context1.SaveChanges();
}