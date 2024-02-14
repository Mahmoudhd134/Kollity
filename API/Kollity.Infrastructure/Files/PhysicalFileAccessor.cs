using Kollity.Application.Abstractions.Files;
using Kollity.Application.Dtos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Kollity.Infrastructure.Files;

public class PhysicalFileAccessor : PhysicalBaseFileAccessor, IFileAccessor
{
    public PhysicalFileAccessor(IWebHostEnvironment webHostEnvironment) : base(
        webHostEnvironment.WebRootPath, Path.Combine("files", "room-content"))
    {
    }

    public Task<FileStreamDto> GetStream(string path)
    {
        var info = new FileInfo(path);
        var stream = File.OpenRead(path);
        return Task.FromResult(new FileStreamDto()
        {
            Stream = stream,
            Size = info.Length
        });
    }
}