using System.ComponentModel.DataAnnotations;

namespace Kollity.Services.Application.Dtos.Student;

public class AddStudentDto
{
    [Required] public string UserName { get; set; }
    [Required] public string FullNameInArabic { get; set; }
    [Required] public string Code { get; set; }
    [Required] public string Password { get; set; }
}