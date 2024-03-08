using Kollity.Application.Abstractions.Files;
using Kollity.Application.Abstractions.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Kollity.Infrastructure.Files;

public class PhysicalProfileImageServices : PhysicalBaseFileAccessor, IProfileImageServices
{
    public PhysicalProfileImageServices(IWebHostEnvironment webHostEnvironment) : base(
        webHostEnvironment.WebRootPath, Path.Combine("files", "profile-images"))
    {
    }

    public Task<string> UploadImage(IFormFile image)
    {
        return UploadFile(image);
    }

    public Task<string> UploadImage(Stream image, string extension)
    {
        return UploadFile(image, extension);
    }


    public async Task<string> ReplaceImage(string path, IFormFile image)
    {
        if (await DeleteImage(path) == false)
            return null;
        return await UploadImage(image);
    }

    public async Task<string> ReplaceImage(string path, Stream image, string extension)
    {
        if (await DeleteImage(path) == false)
            return null;
        return await UploadImage(image, extension);
    }


    public Task<bool> DeleteImage(string path)
    {
        return Delete(path);
    }
}