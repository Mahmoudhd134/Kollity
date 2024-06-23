using Kollity.Exams.Domain.ExamModels;

namespace Kollity.Exams.Domain.RoomModels;

public class Room
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid DoctorId { get; set; }
    public User Doctor { get; set; }
    public bool IsDeleted { get; set; }

    public List<RoomUser> RoomUsers { get; set; } = [];
    public List<Exam> Exams { get; set; } = [];
    public List<ExamAnswer> ExamAnswers { get; set; } = [];
}