using Kollity.Common.ErrorHandling;

namespace Kollity.User.API.Helpers;

public class UserFailureType
{
    public string Type { get; set; }
    public string Title { get; set; }
    public int Status { get; set; }
    public List<Error> Errors { get; set; }
}