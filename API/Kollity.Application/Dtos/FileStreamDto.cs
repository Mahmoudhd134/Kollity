namespace Kollity.Application.Dtos;

public class FileStreamDto
{
    public string Name { get; set; }
    public string Extension { get; set; }
    public double Size { get; set; }
    public Stream Stream { get; set; }
}