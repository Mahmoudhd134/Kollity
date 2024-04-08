namespace Kollity.Services.Application.Dtos.Identity;

public class ChangeImagePhotoDto
{
    // [Required] public IFormFile Image { get; set; }
    public Stream ImageStream { get; set; }
    public string Extensions { get; set; }
}