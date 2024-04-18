using Kollity.Reporting.Domain.RoomModels;
using Kollity.Reporting.Domain.UserModels;

namespace Kollity.Reporting.Domain.ExamModels;

public class Exam
{
    public Guid Id { get; set; }
    public Guid ExamId { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime CreationDate { get; set; }

    public Guid DoctorId { get; set; }
    public Doctor Doctor { get; set; }

    public Guid RoomId { get; set; }
    public Room Room { get; set; }

    public Guid? QuestionId { get; set; }
    public string QuestionText { get; set; }
    public int? QuestionOpenForSeconds { get; set; }
    public byte? QuestionDegree { get; set; }

    public Guid? OptionId { get; set; }
    public string Option { get; set; }
    public bool? IsRightOption { get; set; }

    public List<ExamAnswer> Answers { get; set; } = [];
}