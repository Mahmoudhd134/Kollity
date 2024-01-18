namespace Domain.AssignmentModels;

public class AssignmentImage
{
    public Guid Id { get; set; }
    public Guid AssignmentId { get; set; }
    public Assignment Assignment { get; set; }
    public string Image { get; set; }
}