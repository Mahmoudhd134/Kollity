using System.ComponentModel.DataAnnotations;

namespace Kollity.Services.Application.Dtos.Course;

public class CourseStudentIdsMap
{
    [Required] public Guid StudentId { get; set; }
    [Required] public Guid CourseId { get; set; }
}