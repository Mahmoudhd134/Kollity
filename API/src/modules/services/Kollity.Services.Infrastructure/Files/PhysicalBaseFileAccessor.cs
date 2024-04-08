using Kollity.Services.Application.Abstractions.Files;
using Microsoft.AspNetCore.Http;

namespace Kollity.Services.Infrastructure.Files;

public abstract class PhysicalBaseFileAccessor
{
    private string _fullPath;
    private string _relativePath;
    private readonly string _rootPath;

    protected PhysicalBaseFileAccessor(string rootPath, string relativePath)
    {
        _rootPath = rootPath;
        _relativePath = relativePath;
        _fullPath = Path.Combine(rootPath, relativePath);
    }

    protected void UpdateFullPath(Category category)
    {
        _relativePath = Path.Combine(_relativePath, category.ToString());
        _fullPath = Path.Combine(_rootPath, _relativePath);
    }

    public virtual async Task<string> UploadFile(Stream file, string extension)
    {
        if (Directory.Exists(_fullPath) == false)
            Directory.CreateDirectory(_fullPath);
        var fileName = $"{Guid.NewGuid()}{extension}";
        var filePath = Path.Combine(_fullPath, fileName);
        await using var fileStream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(fileStream);
        return Path.Combine(_relativePath, fileName);
    }

    public virtual async Task<string> UploadFile(IFormFile file)
    {
        if (Directory.Exists(_fullPath) == false)
            Directory.CreateDirectory(_fullPath);
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var filePath = Path.Combine(_fullPath, fileName);
        await using var fileStream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(fileStream);
        return Path.Combine(_relativePath, fileName);
    }

    public virtual Task<bool> Delete(string path)
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