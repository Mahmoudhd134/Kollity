namespace Kollity.Services.Contracts.Assignment;

public class AssignmentFileAddedIntegrationEvent
{
    public Guid AssignmentId { get; set; }
    public Guid FileId { get; set; }
    public string FileName { get; set; }
    public DateTime UploadDate { get; set; }
}