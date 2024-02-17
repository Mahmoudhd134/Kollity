using Bogus;
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
var connectionString2 = "server=sql.bsite.net\\MSSQL2016;database=mahmoudhd1345_;user id=mahmoudhd1345_;password=Freeasphosting;encrypt=false;";
IServiceProvider provider = new ServiceCollection()
    .AddPersistenceConfigurations(connectionString2)
    .AddInfrastructureServices()
    .BuildServiceProvider();

var context = provider.GetRequiredService<ApplicationDbContext>();
context.Database.Migrate();
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
Console.WriteLine();

var students = Enumerable.Range(1, 500)
    .Select(x => new Student
    {
        UserName = $"MahmoudStudent{x}",
        NormalizedUserName = $"MahmoudStudent{x}".ToUpper(),
        FullNameInArabic = $"this is my student{x}'th full name",
        Email = $"this_is_my_student_email_{x}@gmail.com",
        NormalizedEmail = $"this_is_my_student_email_{x}@gmail.com".ToUpper(),
        Code = x.ToString(),
        Roles =
        [
            new IdentityUserRole<Guid>
            {
                RoleId = studentRole.Id
            }
        ]
    })
    .ToList();
students.ForEach(x => x.PasswordHash = studentManager.PasswordHasher.HashPassword(x, "Mahmoud2320030@"));


var assistants = Enumerable.Range(1, 50)
    .Select(x => new Doctor
    {
        UserName = $"MahmoudAssistant{x}",
        NormalizedUserName = $"MahmoudAssistant{x}".ToUpper(),
        Email = $"this_is_my_assistant_email_{x}@gmail.com",
        NormalizedEmail = $"this_is_my_assistant_email_{x}@gmail.com".ToUpper(),
        FullNameInArabic = $"this is my assistant{x}'th full name",
        Roles =
        [
            new IdentityUserRole<Guid>
            {
                RoleId = assistantRole.Id
            }
        ]
    })
    .ToList();
assistants.ForEach(x => x.PasswordHash = doctorManager.PasswordHasher.HashPassword(x, "Mahmoud2320030@"));


var doctors = Enumerable.Range(1, 25)
    .Select(x => new Doctor
    {
        UserName = $"MahmoudDoctor{x}",
        NormalizedUserName = $"MahmoudDoctor{x}".ToUpper(),
        FullNameInArabic = $"this is my doctor{x}'th full name",
        Email = $"this_is_my_doctor_email_{x}@gmail.com",
        NormalizedEmail = $"this_is_my_doctor_email_{x}@gmail.com".ToUpper(),
        Roles =
        [
            new IdentityUserRole<Guid>
            {
                RoleId = doctorRole.Id
            }
        ]
    })
    .ToList();
doctors.ForEach(x => x.PasswordHash = doctorManager.PasswordHasher.HashPassword(x, "Mahmoud2320030@"));

context.Students.AddRange(students);
context.Doctors.AddRange(doctors);
context.Doctors.AddRange(assistants);
await context.SaveChangesAsync();
Console.WriteLine("Add Students, Doctors and Assistatns");

var studentsId = students.Select(x => x.Id).ToList();
var doctorsId = doctors.Select(x => x.Id).ToList();
var assistantsId = assistants.Select(x => x.Id).ToList();

List<string> deps = ["CS", "IT", "IS", "MM"];
var courseFaker = new Faker<Course>()
    .RuleFor(x => x.Name, f => f.Commerce.ProductName())
    .RuleFor(x => x.Department, f => deps[f.Random.Int(0, 3)])
    .RuleFor(x => x.Hours, f => f.Random.Int(1, 3))
    .RuleFor(x => x.Code, f => f.Random.Int(100, 499))
    .RuleFor(x => x.DoctorId, f => f.Random.Int(0, 9) > 5 ? doctorsId[f.Random.Int(0, doctorsId.Count - 1)] : null)
    .RuleFor(x => x.CoursesAssistants, f =>
    {
        var p = f.Random.Int(0, 9);
        if (p > 5)
            return null;

        return Enumerable.Range(0, f.Random.Int(1, 5))
            .Select(x => new CourseAssistant()
            {
                AssistantId = assistantsId[f.Random.Int(0, assistantsId.Count - 1)]
            })
            .DistinctBy(x => x.AssistantId)
            .ToList();
    })
    .RuleFor(x => x.StudentsCourses, f =>
    {
        var numberOfStudents = f.Random.Int(50, studentsId.Count - 1);
        return Enumerable.Range(0, numberOfStudents)
            .Select(x => new StudentCourse()
            {
                StudentId = studentsId[f.Random.Int(0, studentsId.Count - 1)]
            })
            .DistinctBy(x => x.StudentId)
            .ToList();
    });

var courses = courseFaker
    .GenerateForever()
    .DistinctBy(x => x.Code)
    .Take(100)
    .ToList();

context.Courses.AddRange(courses);
await context.SaveChangesAsync();
Console.WriteLine("Add Courses");

courses.ForEach(c =>
{
    if (c.CoursesAssistants is null || c.CoursesAssistants.Count == 0)
        return;

    var ass = c.CoursesAssistants.First();
    c.Rooms.Add(new Room
    {
        Name = $"this is room for assistant -{ass.AssistantId}",
        DoctorId = ass.AssistantId,
        EnsureJoinRequest = false
    });
});
await context.SaveChangesAsync();
Console.WriteLine("Update Courses");


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