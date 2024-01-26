using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Course;

public class AddCourseDto
{
    [Required] public string Department { get; set; }
    [Required] public int Code { get; set; }
    [Required] public int Hours { get; set; }
    [Required] public string Name { get; set; }
}