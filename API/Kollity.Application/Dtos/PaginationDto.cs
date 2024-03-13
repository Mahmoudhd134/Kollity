using System.ComponentModel.DataAnnotations;

namespace Kollity.Application.Dtos;

public class PaginationDto
{
    [Required] public int PageIndex { get; set; }
    [Required] public int PageSize { get; set; }
}