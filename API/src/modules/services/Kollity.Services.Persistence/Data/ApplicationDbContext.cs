using Kollity.Services.Domain.AssignmentModels;
using Kollity.Services.Domain.AssignmentModels.AssignmentGroupModels;
using Kollity.Services.Domain.CourseModels;
using Kollity.Services.Domain.DoctorModels;
using Kollity.Services.Domain.Identity;
using Kollity.Services.Domain.Messages;
using Kollity.Services.Domain.RoomModels;
using Kollity.Services.Domain.StudentModels;
using Kollity.Services.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Persistence.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<BaseUser> Users { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Room> Rooms { get; set; }

    public DbSet<RoomMessage> RoomMessages { get; set; }
    public DbSet<MessagePollAnswer> RoomMessagePollAnswers { get; set; }

    public DbSet<RoomContent> RoomContents { get; set; }
    public DbSet<UserRoom> UserRooms { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseAssistant> CourseAssistants { get; set; }
    public DbSet<StudentCourse> StudentCourses { get; set; }
    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<AssignmentFile> AssignmentFiles { get; set; }
    public DbSet<AssignmentAnswer> AssignmentAnswers { get; set; }
    public DbSet<AssignmentGroup> AssignmentGroups { get; set; }
    public DbSet<AssignmentGroupStudent> AssignmentGroupStudents { get; set; }
    public DbSet<AssignmentAnswerDegree> AssignmentAnswerDegrees { get; set; }

    public DbSet<OutboxMessage> OutboxMessages { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSnakeCaseNamingConvention();
        // optionsBuilder.UseSqlServer("server=.;database=MyCollege;trusted_connection=true;encrypt=false;");
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("services");
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(PersistenceExtensions).Assembly);
        builder.ApplyUtcDateTimeConverter();
    }
}