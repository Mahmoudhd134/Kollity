using Domain.AssistantModels;

namespace Domain.CourseModels;

public class CourseAssistant
{
    public Guid Id { get; set; }

    public Guid CourseId { get; set; }
    public Course Course { get; set; }

    public Guid AssistantId { get; set; }
    public Assistant Assistant { get; set; }
}