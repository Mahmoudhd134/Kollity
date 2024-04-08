using System.ComponentModel.DataAnnotations;

namespace Kollity.Services.Application.Dtos.Assignment.Group;

public class AssignmentGroupInvitationMapDto
{
    [Required] public Guid StudentId { get; set; }
    [Required] public Guid GroupId { get; set; }
}