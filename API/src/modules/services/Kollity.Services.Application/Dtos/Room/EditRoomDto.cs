using System.ComponentModel.DataAnnotations;

namespace Kollity.Services.Application.Dtos.Room;

public class EditRoomDto
{
    [Required] public Guid Id { get; set; }
    [Required] public string Name { get; set; }
    [Required] public bool EnsureJoinRequest { get; set; }
    [Required] public byte AssignmentGroupMaxLength { get; set; }
    [Required] public bool AssignmentGroupOperationsEnabled { get; set; }
}