using Kollity.Reporting.Domain.UserModels;

namespace Kollity.Reporting.Domain.ExamModels;

public class ExamAnswer
{
    public Guid ExamId { get; set; }
    public Exam Exam { get; set; }
    public Guid StudentId { get; set; }
    public User Student { get; set; }
    public DateTime RequestTime { get; set; }
    public DateTime? SubmitTime { get; set; }
}