using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Doctor;

public class AddDoctorDto
{
    [Required] public string UserName { get; set; }
    [Required] public string Password { get; set; }

    [Required, AllowedValues(Domain.Identity.Role.Role.Doctor,Domain.Identity.Role.Role.Assistant)]
    public string Role { get; set; }
}