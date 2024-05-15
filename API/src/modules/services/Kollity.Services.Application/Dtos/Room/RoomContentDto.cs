using Kollity.Services.Application.Dtos.Identity;

namespace Kollity.Services.Application.Dtos.Room;

public class RoomContentDto
{
    public Guid Id { get; set; }
    public string FilePath { get; set; }
    public string Name { get; set; }
    public DateTime UploadTime { get; set; }
    public BaseUserDto Uploader { get; set; }
}