using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Course;

public class EditCourseDto
{
    [Required] public Guid Id { get; set; }
    public string Department { get; set; }
    public int Code { get; set; }
    public int Hours { get; set; }
    public string Name { get; set; }
}