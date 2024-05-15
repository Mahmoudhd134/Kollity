using Microsoft.AspNetCore.Http;

namespace Kollity.Services.Application.Dtos.Assignment.Group;

public class AddAssignmentFileDto
{
    public IFormFile File { get; set; }
}