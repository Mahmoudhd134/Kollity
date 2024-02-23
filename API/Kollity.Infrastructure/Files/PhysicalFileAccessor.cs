﻿using Kollity.Application.Abstractions.Files;
using Kollity.Application.Dtos;
using Microsoft.AspNetCore.Hosting;

namespace Kollity.Infrastructure.Files;

public class PhysicalFileAccessor : PhysicalBaseFileAccessor, IFileAccessor
{
    public PhysicalFileAccessor(IWebHostEnvironment webHostEnvironment) : base(
        webHostEnvironment.WebRootPath, "files")
    {
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