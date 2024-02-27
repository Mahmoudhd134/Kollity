using System.ComponentModel.DataAnnotations;

namespace Kollity.Application.Dtos.Assignment;

public class AssignmentAnswersFilters
{
    public string StudentFullName { get; set; }
    public string StudentCode { get; set; }
    [Required] public int PageIndex { get; set; }
    [Required] public int PageSize { get; set; }
}