using Domain.AssignmentModels;
using Domain.AssignmentModels.AssignmentGroupModels;
using Domain.CourseModels;
using Domain.DoctorModels;
using Domain.ExamModels;
using Domain.Identity;
using Domain.Identity.Role;
using Domain.Identity.User;
using Domain.Identity.UserRefreshToken;
using Domain.RoomModels;
using Domain.StudentModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.Extensions;

namespace Persistence.Data;

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
    // public DbSet<RoomSupervisor> RoomSupervisors { get; set; }
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