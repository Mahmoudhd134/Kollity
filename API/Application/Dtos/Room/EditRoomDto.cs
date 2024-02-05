using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Room;

public class EditRoomDto
{
    [Required] public Guid Id { get; set; }
    [Required] public string Name { get; set; }
    [Required] public bool EnsureJoinRequest { get; set; }
}