using System.IO;
using Application.Abstractions.Files;
using Microsoft.AspNetCore.Hosting;

namespace Infrastructure.Files;

public class PhysicalImageAccessor : PhysicalBaseFileAccessor, IImageAccessor
{
    public PhysicalImageAccessor(IWebHostEnvironment webHostEnvironment) : base(
        webHostEnvironment.WebRootPath, Path.Combine("files", "images"))
    {
    }
}