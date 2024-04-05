using System.ComponentModel.DataAnnotations;
using Kollity.Contracts.Assignment;
using Kollity.Domain.AssignmentModels;

namespace Kollity.Application.Dtos.Assignment;

public class AddAssignmentDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime OpenUntilDate { get; set; }
    public byte Degree { get; set; }

    [EnumDataType(typeof(AssignmentMode))] public AssignmentMode Mode { get; set; }
}