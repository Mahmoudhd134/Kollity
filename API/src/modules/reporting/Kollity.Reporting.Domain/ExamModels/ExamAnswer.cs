using Kollity.Reporting.Domain.UserModels;

namespace Kollity.Reporting.Domain.ExamModels;

public class ExamAnswer
{
    public Guid OptionId { get; set; }
    public Exam Option { get; set; }
    public Guid StudentId { get; set; }
    public Student Student { get; set; }
    public DateTime RequestTime { get; set; }
    public DateTime? SubmitTime { get; set; }
}