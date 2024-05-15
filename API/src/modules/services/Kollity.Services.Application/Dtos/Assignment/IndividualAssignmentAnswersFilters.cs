using System.ComponentModel.DataAnnotations;

namespace Kollity.Services.Application.Dtos.Assignment;

public class IndividualAssignmentAnswersFilters
{
    public string StudentFullName { get; set; }
    public string StudentCode { get; set; }
    [Required] public int PageIndex { get; set; }
    [Required] public int PageSize { get; set; }
}