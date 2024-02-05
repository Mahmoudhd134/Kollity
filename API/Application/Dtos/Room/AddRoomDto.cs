using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Room;

public class AddRoomDto
{
    [Required] public Guid CourseId { get; set; }
    [Required] public string Name { get; set; }
    [Required, DefaultValue(false)] public bool EnsureJoinRequest { get; set; }
}