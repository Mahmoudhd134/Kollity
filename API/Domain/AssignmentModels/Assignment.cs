namespace Domain.AssignmentModels;

public class Assignment
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public AssignmentMode Mode { get; set; }
    public DateTime CreatedDate { get; set; }
    public List<AssignmentImage> AssignmentImages { get; set; } = [];
    public List<AssignmentAnswer> AssignmentsAnswers { get; set; } = [];
}