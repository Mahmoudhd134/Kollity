using Microsoft.AspNetCore.Http;

namespace Kollity.Application.Abstractions.Files;

public interface IImageAccessor
{
    Task<string> UploadImage(IFormFile image);
    Task<string> ReplaceImage(string path, IFormFile image);
    Task<bool> DeleteImage(string path);
}