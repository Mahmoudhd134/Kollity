namespace Kollity.Application.Dtos.Room;

public class RoomMemberDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string ProfileImage { get; set; }
    public string FullName { get; set; }
    public bool IsSupervisor { get; set; }
    public UserRoomState State { get; set; }
}