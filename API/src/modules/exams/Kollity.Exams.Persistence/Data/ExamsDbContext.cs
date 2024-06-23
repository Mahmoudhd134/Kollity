using Kollity.Exams.Domain;
using Kollity.Exams.Domain.ExamModels;
using Kollity.Exams.Domain.RoomModels;
using Kollity.Exams.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Exams.Persistence.Data;

public class ExamsDbContext(DbContextOptions<ExamsDbContext> options) : DbContext(options)
{
    public DbSet<Exam> Exams { get; set; }
    public DbSet<ExamQuestion> ExamQuestions { get; set; }
    public DbSet<ExamQuestionOption> ExamQuestionOptions { get; set; }
    public DbSet<ExamAnswer> ExamAnswers { get; set; }
    public DbSet<Room> Rooms { get; set; }

    public DbSet<RoomUser> RoomUsers { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("exams");
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(ExamsDbContext).Assembly);
        builder.ApplyUtcDateTimeConverter();
    }
}