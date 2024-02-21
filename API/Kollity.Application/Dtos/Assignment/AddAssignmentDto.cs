using System.ComponentModel.DataAnnotations;
using Kollity.Domain.AssignmentModels;

namespace Kollity.Application.Dtos.Assignment;

public class AddAssignmentDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    [EnumDataType(typeof(AssignmentMode))]
    public AssignmentMode Mode { get; set; }
}