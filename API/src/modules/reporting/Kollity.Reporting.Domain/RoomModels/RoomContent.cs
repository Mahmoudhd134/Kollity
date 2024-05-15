using Kollity.Reporting.Domain.UserModels;

namespace Kollity.Reporting.Domain.RoomModels;

public class RoomContent
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime UploadTime { get; set; }

    public User Uploader { get; set; }
    public Guid UploaderId { get; set; }

    public Guid RoomId { get; set; }
    public Room Room { get; set; }
}