using Kollity.Application;
using Kollity.Domain.AssignmentModels;
using Kollity.Domain.AssignmentModels.AssignmentGroupModels;
using Kollity.Domain.CourseModels;
using Kollity.Domain.DoctorModels;
using Kollity.Domain.ExamModels;
using Kollity.Domain.RoomModels;
using Kollity.Domain.StudentModels;
using Kollity.Infrastructure;
using Kollity.Persistence;
using Kollity.Persistence.Data;
using Kollity.Persistence.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var connectionString = "server=.;database=MyCollege;trusted_connection=true;encrypt=false;";
IServiceProvider provider = new ServiceCollection()
    .AddPersistenceConfigurations(connectionString)
    .AddInfrastructureServices()
    .BuildServiceProvider();

var context = provider.GetRequiredService<ApplicationDbContext>();
var studentManager = provider.GetRequiredService<UserManager<Student>>();
var doctorManager = provider.GetRequiredService<UserManager<Doctor>>();

var studentRole = context.Roles.First(x => x.Name == "Student");
var assistantRole = context.Roles.First(x => x.Name == "Assistant");
var doctorRole = context.Roles.First(x => x.Name == "Doctor");

// var s = new Student()
// {
//     UserName = "mah",
//     NormalizedUserName = "MAH",
//     FullNameInArabic = "afs",
//     Code = "af",
//     // ConcurrencyStamp = Guid.NewGuid().ToString()
// };
// var passwordHash = studentManager.PasswordHasher.HashPassword(s, "Mahmoud2320030@");
// s.PasswordHash = passwordHash;
// context.Add(s);
// context.SaveChanges();
// var ss = await studentManager.FindByNameAsync("mah");
// var x = await studentManager.CheckPasswordAsync(ss, "Mahmoud2320030@");
// await studentManager.UpdateSecurityStampAsync(ss);
// Console.WriteLine();

// Enumerable.Range(1, 100)
//     .Select(x => new Student
//     {
//         UserName = $"student_username{x}",
//         FullNameInArabic = $"اسم عربي {x}",
//         Email = $"emailbyme{x}@qalbi.bahbeek",
//         Code = x.ToString(),
//         Roles =
//         [
//             new IdentityUserRole<Guid>
//             {
//                 RoleId = studentRole.Id
//             }
//         ]
//     })
//     .Select(x => studentManager.CreateAsync(x, "Mahmoud2320030@").Result)
//     .ToList();


Enumerable.Range(1, 50)
    .Select(x => new Doctor
    {
        UserName = $"MahmoudAssistant{x}",
        FullNameInArabic = $"اسم عربي {x}",
        Email = $"emailbyme{x}@qalbiyadoctor.bahbeek",
        Roles =
        [
            new IdentityUserRole<Guid>
            {
                RoleId = assistantRole.Id
            }
        ]
    })
    .Select(x => doctorManager.CreateAsync(x, "Mahmoud2320030@").Result)
    .ToList();


Enumerable.Range(1, 25)
    .Select(x => new Doctor
    {
        UserName = $"MahmoudDoctor{x}",
        FullNameInArabic = $"اسم عربي {x}",
        Email = $"emailbyme{x}@qalbiyamoeed.bahbeek",
        Roles =
        [
            new IdentityUserRole<Guid>
            {
                RoleId = doctorRole.Id
            }
        ]
    })
    .Select(x => doctorManager.CreateAsync(x, "Mahmoud2320030@").Result)
    .ToList();

// CreateCourse(context);
// CreateAssignment(context);

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

    var exam = new Exam
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
                    new ExamQuestionOption { Option = "all above", IsRightOption = true }
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
                StudentId = student.Id
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