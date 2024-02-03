using Application.Abstractions.Files;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Files;

public class PhysicalImageAccessor : PhysicalBaseFileAccessor, IImageAccessor
{
    public PhysicalImageAccessor(IWebHostEnvironment webHostEnvironment) : base(
        webHostEnvironment.WebRootPath, Path.Combine("files", "images"))
    {
    }

    public Task<string> UploadImage(IFormFile image) =>
        UploadFile(image);

    public async Task<string> ReplaceImage(string path, IFormFile image)
    {
        if (await DeleteImage(path) == false)
            return null;
        return await UploadImage(image);
    }

    public Task<bool> DeleteImage(string path) =>
        DeleteFile(path);
}