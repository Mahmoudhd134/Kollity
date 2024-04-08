using Kollity.Services.Application.Abstractions.Files;
using Kollity.Services.Application.Dtos;
using Microsoft.AspNetCore.Http;

namespace Kollity.Services.Application.Abstractions.Services;

public interface IFileServices
{
    Task<string> UploadFile(Stream file, string extension, Category category);
    Task<string> UploadFile(IFormFile file, Category category);
    Task<bool> Delete(string path);
    Task<bool> Delete(List<string> paths);
    Task<FileStreamDto> GetStream(string path);
}