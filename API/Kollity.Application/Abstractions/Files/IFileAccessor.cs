using Kollity.Application.Dtos;
using Microsoft.AspNetCore.Http;

namespace Kollity.Application.Abstractions.Files;

public interface IFileAccessor
{
    Task<string> UploadFile(Stream file, string extension);
    Task<string> UploadFile(IFormFile file);
    Task<bool> DeleteFile(string path);
    Task<FileStreamDto> GetStream(string path);
}