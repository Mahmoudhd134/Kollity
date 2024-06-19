using Kollity.Feedback.Domain;
using Kollity.Feedback.Domain.FeedbackModels;
using Kollity.Feedback.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Feedback.Persistence.Data;

public class FeedbackDbContext(DbContextOptions<FeedbackDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseStudent> CourseStudents { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<RoomUser> RoomUsers { get; set; }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<FeedbackQuestion> FeedbackQuestions { get; set; }
    public DbSet<FeedbackAnswer> FeedbackAnswers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("feedback");
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FeedbackDbContext).Assembly);
        modelBuilder.ApplyUtcDateTimeConverter();
        var foreignKeys = modelBuilder.Model
            .GetEntityTypes()
            .SelectMany(x => x.GetForeignKeys());
        foreach (var foreignKey in foreignKeys)
        {
            foreignKey.DeleteBehavior = DeleteBehavior.NoAction;
        }
    }
}