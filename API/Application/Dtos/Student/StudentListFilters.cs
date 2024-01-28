using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Student;

public class StudentListFilters
{
    [Required] public int PageIndex { get; set; }
    [Required] public int PageSize { get; set; }
    public string Code { get; set; }
    public string UserNamePrefix { get; set; }
}