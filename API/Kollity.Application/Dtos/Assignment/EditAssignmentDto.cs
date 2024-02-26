using System.ComponentModel.DataAnnotations;
using Kollity.Domain.AssignmentModels;

namespace Kollity.Application.Dtos.Assignment;

public class EditAssignmentDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime OpenUntilDate { get; set; }

    [EnumDataType(typeof(AssignmentMode))] public AssignmentMode Mode { get; set; }
}