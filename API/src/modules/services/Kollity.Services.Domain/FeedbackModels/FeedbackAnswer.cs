using Kollity.Services.Domain.CourseModels;
using Kollity.Services.Domain.DoctorModels;
using Kollity.Services.Domain.ExamModels;
using Kollity.Services.Domain.StudentModels;

namespace Kollity.Services.Domain.FeedbackModels;

public class FeedbackAnswer
{
    public Guid Id { get; set; }

    public Guid StudentId { get; set; }
    public Student Student { get; set; }
    public Guid QuestionId { get; set; }
    public FeedbackQuestion Question { get; set; }
    
    public FeedbackQuestionAnswer? Answer { get; set; }
    public string StringAnswer { get; set; }

    public Guid? CourseId { get; set; }
    public Course Course { get; set; }

    public Guid? DoctorId { get; set; }
    public Doctor Doctor { get; set; }

    public Guid? ExamId { get; set; }
    public Exam Exam { get; set; }
}