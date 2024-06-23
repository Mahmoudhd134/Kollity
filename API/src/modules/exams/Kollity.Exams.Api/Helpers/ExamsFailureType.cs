using Kollity.Common.ErrorHandling;

namespace Kollity.Exams.Api.Helpers;

public class ExamsFailureType
{
    public string Type { get; set; }
    public string Title { get; set; }
    public int Status { get; set; }
    public List<Error> Errors { get; set; }
}