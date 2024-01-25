using Domain.CourseModels;
using Domain.RoomModels;

namespace Domain.ExamModels;

public class Exam
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime LastUpdatedDate { get; set; }

    public Guid CourseId { get; set; }
    public Course Course { get; set; }
    
    public Guid RoomId { get; set; }
    public Room Room { get; set; }
    
    public List<ExamQuestion> ExamQuestions { get; set; } = [];
}