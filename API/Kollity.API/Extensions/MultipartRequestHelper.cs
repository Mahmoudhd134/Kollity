using Kollity.Domain.ErrorHandlers;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;

namespace Kollity.API.Extensions;

public static class MultipartRequestHelper
{
    public static async Task<Result<HttpMultipartSectionFileInfo>> GetSectionAndFileInfos(this HttpRequest request,
        long lengthLimit)
    {
        if (!MultipartRequestHelper.IsMultipartContentType(request.ContentType))
            return Error.Validation("UploadFile", "Not a multipart request");

        var boundary = MultipartRequestHelper.GetBoundary(MediaTypeHeaderValue.Parse(request.ContentType), lengthLimit);
        if (boundary.IsSuccess == false)
            return boundary.Errors;
        var reader = new MultipartReader(boundary.Data, request.Body);

        // note: this is for a single file, you could also process multiple files
        var section = await reader.ReadNextSectionAsync();

        if (section == null)
            return Error.Validation("UploadFile", "No sections in multipart defined");

        if (!ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out var contentDisposition))
            return Error.Validation("UploadFile", "No content disposition in multipart defined");

        var fileName = contentDisposition.FileNameStar.ToString();
        if (string.IsNullOrEmpty(fileName))
        {
            fileName = contentDisposition.FileName.ToString();
        }

        if (string.IsNullOrEmpty(fileName))
            return Error.Validation("UploadFile", "No filename defined.");

        return new HttpMultipartSectionFileInfo(section, fileName);
    }

    public static Result<string> GetBoundary(MediaTypeHeaderValue contentType, long lengthLimit)
    {
        var boundary = HeaderUtilities.RemoveQuotes(contentType.Boundary).Value;

        if (string.IsNullOrWhiteSpace(boundary))
            return Error.Validation("UploadFile", "Missing content-type boundary.");

        if (boundary.Length > lengthLimit)
            return Error.Validation("UploadFile", $"Multipart boundary length limit {lengthLimit} exceeded.");

        return boundary;
    }

    public static bool IsMultipartContentType(string contentType)
    {
        return !string.IsNullOrEmpty(contentType)
               && contentType.IndexOf("multipart/", StringComparison.OrdinalIgnoreCase) >= 0;
    }

    public static bool HasFormDataContentDisposition(ContentDispositionHeaderValue contentDisposition)
    {
        // Content-Disposition: form-data; name="key";
        return contentDisposition != null
               && contentDisposition.DispositionType.Equals("form-data")
               && string.IsNullOrEmpty(contentDisposition.FileName.Value)
               && string.IsNullOrEmpty(contentDisposition.FileNameStar.Value);
    }

    public static bool HasFileContentDisposition(ContentDispositionHeaderValue contentDisposition)
    {
        // Content-Disposition: form-data; name="myfile1"; filename="Misc 002.jpg"
        return contentDisposition != null
               && contentDisposition.DispositionType.Equals("form-data")
               && (!string.IsNullOrEmpty(contentDisposition.FileName.Value)
                   || !string.IsNullOrEmpty(contentDisposition.FileNameStar.Value));
    }
}