using Kollity.Reporting.Domain.AssignmentModels;
using Kollity.Reporting.Domain.CourseModels;
using Kollity.Reporting.Domain.ExamModels;
using Kollity.Reporting.Domain.RoomModels;
using Kollity.Reporting.Domain.UserModels;
using Kollity.Reporting.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Reporting.Persistence.Data;

public class ReportingDbContext : DbContext
{
    public ReportingDbContext(DbContextOptions<ReportingDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseDoctorAndAssistants> CourseDoctorAndAssistants { get; set; }
    public DbSet<CourseStudent> CourseStudents { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<RoomUser> RoomUsers { get; set; }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<ExamAnswer> ExamAnswers { get; set; }
    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<AssignmentGroup> AssignmentGroups { get; set; }
    public DbSet<AssignmentAnswer> AssignmentAnswers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("reporting");
        base.OnModelCreating(builder);
        var foreignKeys = builder.Model
            .GetEntityTypes()
            .SelectMany(x => x.GetForeignKeys());
        foreach (var foreignKey in foreignKeys)
        {
            foreignKey.DeleteBehavior = DeleteBehavior.NoAction;
        }

        builder.ApplyConfigurationsFromAssembly(typeof(PersistenceExtensions).Assembly);
        builder.ApplyUtcDateTimeConverter();
    }
}