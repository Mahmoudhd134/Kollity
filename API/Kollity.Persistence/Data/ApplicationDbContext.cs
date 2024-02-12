using Kollity.Domain.AssignmentModels;
using Kollity.Domain.AssignmentModels.AssignmentGroupModels;
using Kollity.Domain.CourseModels;
using Kollity.Domain.DoctorModels;
using Kollity.Domain.ExamModels;
using Kollity.Domain.Identity.Role;
using Kollity.Domain.Identity.User;
using Kollity.Domain.Identity.UserRefreshToken;
using Kollity.Domain.RoomModels;
using Kollity.Domain.StudentModels;
using Kollity.Persistence.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Persistence.Data;

public class ApplicationDbContext : IdentityDbContext<BaseUser, BaseRole, Guid>
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
    public DbSet<Room> Rooms { get; set; }

    public DbSet<RoomMessage> RoomMessages { get; set; }

    public DbSet<RoomContent> RoomContents { get; set; }
    public DbSet<UserRoom> UserRooms { get; set; }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<ExamQuestion> ExamQuestions { get; set; }
    public DbSet<ExamQuestionOption> ExamQuestionOptions { get; set; }
    public DbSet<ExamAnswer> ExamAnswers { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseAssistant> CourseAssistants { get; set; }
    public DbSet<StudentCourse> StudentCourses { get; set; }
    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<AssignmentImage> AssignmentImages { get; set; }
    public DbSet<AssignmentAnswer> AssignmentAnswers { get; set; }
    public DbSet<AssignmentGroup> AssignmentGroups { get; set; }
    public DbSet<AssignmentGroupStudent> AssignmentGroupStudents { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSnakeCaseNamingConvention();
        // optionsBuilder.UseSqlServer("server=.;database=MyCollege;trusted_connection=true;encrypt=false;");
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(PersistenceExtensions).Assembly);
        builder.ApplyUtcDateTimeConverter();
    }
}