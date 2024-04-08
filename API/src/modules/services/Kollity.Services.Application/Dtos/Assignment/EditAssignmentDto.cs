using System.ComponentModel.DataAnnotations;
using Kollity.Services.Contracts.Assignment;
using Kollity.Services.Domain.AssignmentModels;

namespace Kollity.Services.Application.Dtos.Assignment;

public class EditAssignmentDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime OpenUntilDate { get; set; }
    public byte Degree { get; set; }

    [EnumDataType(typeof(AssignmentMode))] public AssignmentMode Mode { get; set; }
}