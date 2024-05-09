using Kollity.Common.ErrorHandling;

namespace Kollity.Reporting.API.Helpers;

public class ReportingFailureType
{
    public string Type { get; set; }
    public string Title { get; set; }
    public int Status { get; set; }
    public List<Error> Errors { get; set; }
}