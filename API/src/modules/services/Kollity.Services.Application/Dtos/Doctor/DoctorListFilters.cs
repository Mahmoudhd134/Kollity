using System.ComponentModel.DataAnnotations;
using Kollity.Services.Domain.Identity;

namespace Kollity.Services.Application.Dtos.Doctor;

public class DoctorListFilters
{
    [Required] public int PageIndex { get; set; }
    [Required] public int PageSize { get; set; }
    public string UserNamePrefix { get; set; }
    public UserType? Type { get; set; }
}