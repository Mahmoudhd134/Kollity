using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Kollity.Services.Application.Dtos.Assignment;

public class AddAssignmentAnswerDto
{
    [Required] public IFormFile File { get; set; }
}