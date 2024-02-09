using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Application.Dtos.Identity;

public class ChangeImagePhotoDto
{
    [Required] public IFormFile Image { get; set; }
}