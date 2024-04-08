using Microsoft.AspNetCore.WebUtilities;

namespace Kollity.Services.API.Extensions;

public record HttpMultipartSectionFileInfo(MultipartSection Section, string FileName);