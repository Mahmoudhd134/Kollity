using System.ComponentModel.DataAnnotations;

namespace Kollity.Application.Dtos.Doctor;

public class AddDoctorDto
{
    [Required] public string UserName { get; set; }
    [Required] public string Password { get; set; }
    [Required] public string FullNameInArabic { get; set; }

    [Required]
    public string Role { get; set; }
}