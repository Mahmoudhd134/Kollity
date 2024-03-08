using Kollity.Domain.ErrorHandlers.Abstractions;

namespace Kollity.API.Helpers;

public class FailureType
{
    public string Type { get; set; }
    public string Title { get; set; }
    public int Status { get; set; }
    public List<Error> Errors { get; set; }
}