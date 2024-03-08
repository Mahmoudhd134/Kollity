namespace Kollity.API.Dtos;

public class FailureResponseType
{
    public string Type { get; set; }
    public int StatusCode { get; set; }
    public Dictionary<string, object> Extensions { get; set; }
}