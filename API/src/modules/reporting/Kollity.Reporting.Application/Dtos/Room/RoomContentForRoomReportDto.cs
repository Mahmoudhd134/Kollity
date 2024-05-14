using Kollity.Reporting.Application.Dtos.User;

namespace Kollity.Reporting.Application.Dtos.Room;

public class RoomContentForRoomReportDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public UserUsernameDto Uploader { get; set; }
    public DateTime UploadedAt { get; set; }
}