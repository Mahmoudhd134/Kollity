using System.ComponentModel.DataAnnotations;

namespace Kollity.Services.Application.Dtos.Room;

public class AddRoomDto
{
    [Required] public Guid CourseId { get; set; }
    [Required] public string Name { get; set; }
}