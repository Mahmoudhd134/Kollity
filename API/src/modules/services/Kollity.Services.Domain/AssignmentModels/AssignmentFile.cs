namespace Kollity.Services.Domain.AssignmentModels;

public class AssignmentFile
{
    public Guid Id { get; set; }
    public Guid AssignmentId { get; set; }
    public Assignment Assignment { get; set; }
    public string Name { get; set; }
    public string FilePath { get; set; }
    public DateTime UploadDate { get; set; }
}