using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Files;

public abstract class PhysicalBaseFileAccessor
{
    private readonly string _dir;
    private readonly string _relativePath;
    private readonly string _wwwroot;

    protected PhysicalBaseFileAccessor(string wwwroot, string relativePath)
    {
        _wwwroot = wwwroot;
        _relativePath = relativePath;
        _dir = Path.Combine(wwwroot, relativePath);
    }

    public async Task<string> UploadImage(IFormFile image)
    {
        if (Directory.Exists(_dir) == false)
            Directory.CreateDirectory(_dir);
        var imageName = $"{Guid.NewGuid()}{Path.GetFileName(image.FileName)}";
        var imagePath = Path.Combine(_dir, imageName);
        await using var fileStream = new FileStream(imagePath, FileMode.Create);
        await image.CopyToAsync(fileStream);
        return Path.Combine(_relativePath, imageName);
    }

    public async Task<string> ReplaceImage(string path, IFormFile image)
    {
        if (await DeleteImage(path) == false)
            return null;
        return await UploadImage(image);
    }

    public Task<bool> DeleteImage(string path)
    {
        var fullPath = Path.Combine(_wwwroot, path);
        if (File.Exists(fullPath) == false)
            return Task.FromResult(false);
        File.Delete(fullPath);
        return Task.FromResult(true);
    }
}