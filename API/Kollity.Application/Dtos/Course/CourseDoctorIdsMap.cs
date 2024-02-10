using System.ComponentModel.DataAnnotations;

namespace Kollity.Application.Dtos.Course;

public class CourseDoctorIdsMap
{
    [Required] public Guid CourseId { get; set; }
    [Required] public Guid DoctorId { get; set; }
}