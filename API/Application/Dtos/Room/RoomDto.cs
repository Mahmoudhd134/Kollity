namespace Application.Dtos.Room;

public class RoomDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public bool EnsureJoinRequest { get; set; }
    public CourseForRoomDto Course { get; set; }
    public DoctorForRoomDto Doctor { get; set; }
    public UserRoomState? UserState { get; set; }
}