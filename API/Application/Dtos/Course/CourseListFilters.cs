using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Course;

public class CourseListFilters
{
    [Required] public int PageIndex { get; set; }
    [Required] public int PageSize { get; set; }
    public string Department { get; set; }
    public int? Code { get; set; }
    public string NamePrefix { get; set; }
    public bool? HasADoctor { get; set; }
}