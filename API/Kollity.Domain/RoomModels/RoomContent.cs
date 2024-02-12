using Kollity.Domain.Identity.User;

namespace Kollity.Domain.RoomModels;

public class RoomContent
{
    public Guid Id { get; set; }
    public string FilePath { get; set; }
    public Guid RoomId { get; set; }
    public Room Room { get; set; }
    public DateTime UploadTime { get; set; }
    public Guid? UploaderId { get; set; }
    public BaseUser Uploader { get; set; }
}