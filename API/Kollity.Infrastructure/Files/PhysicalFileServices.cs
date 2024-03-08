using Kollity.Application.Abstractions.Files;
using Kollity.Application.Abstractions.Services;
using Kollity.Application.Dtos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Kollity.Infrastructure.Files;

public class PhysicalFileServices : PhysicalBaseFileAccessor, IFileServices
{
    public PhysicalFileServices(IWebHostEnvironment webHostEnvironment) : base(
        webHostEnvironment.WebRootPath, "files")
    {
    }

    public Task<string> UploadFile(Stream file, string extension, Category category)
    {
        base.UpdateFullPath(category);
        return base.UploadFile(file, extension);
    }

    public Task<string> UploadFile(IFormFile file, Category category)
    {
        base.UpdateFullPath(category);
        return base.UploadFile(file);
    }

    public async Task<bool> Delete(List<string> paths)
    {
        foreach (var path in paths)
            if (await Delete(path) == false)
                return false;

        return true;
    }

    public Task<FileStreamDto> GetStream(string path)
    {
        var info = new FileInfo(path);
        var stream = File.OpenRead(path);
        return Task.FromResult(new FileStreamDto
        {
            Stream = stream,
            Size = info.Length,
            Name = info.Name,
            Extension = info.Extension
        });
    }
}