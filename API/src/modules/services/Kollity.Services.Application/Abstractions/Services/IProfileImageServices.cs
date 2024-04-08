using Microsoft.AspNetCore.Http;

namespace Kollity.Services.Application.Abstractions.Services;

public interface IProfileImageServices
{
    Task<string> UploadImage(IFormFile image);
    Task<string> UploadImage(Stream image, string extension);
    Task<string> ReplaceImage(string path, IFormFile image);
    Task<string> ReplaceImage(string path, Stream image, string extension);
    Task<bool> DeleteImage(string path);
}