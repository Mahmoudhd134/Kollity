using Microsoft.AspNetCore.WebUtilities;

namespace Kollity.API.Extensions;

public record HttpMultipartSectionFileInfo(MultipartSection Section, string FileName);