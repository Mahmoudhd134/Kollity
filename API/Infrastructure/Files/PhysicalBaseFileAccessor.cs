using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Files;

public abstract class PhysicalBaseFileAccessor
{
    private readonly string _fullPath;
    private readonly string _relativePath;
    private readonly string _rootPath;

    protected PhysicalBaseFileAccessor(string rootPath, string relativePath)
    {
        _rootPath = rootPath;
        _relativePath = relativePath;
        _fullPath = Path.Combine(rootPath, relativePath);
    }

    protected async Task<string> UploadFile(IFormFile file)
    {
        if (Directory.Exists(_fullPath) == false)
            Directory.CreateDirectory(_fullPath);
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var filePath = Path.Combine(_fullPath, fileName);
        await using var fileStream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(fileStream);
        return Path.Combine(_relativePath, fileName);
    }

    protected Task<bool> DeleteFile(string path)
    {
        try
        {
            var fullPath = Path.Combine(_rootPath, path);
            if (File.Exists(fullPath) == false)
                return Task.FromResult(false);
            File.Delete(fullPath);
            return Task.FromResult(true);
        }
        catch
        {
            return Task.FromResult(false);
        }
    }
}