namespace Kollity.Services.API.Helpers;

public class ServicesFailureType
{
    public string Type { get; set; }
    public string Title { get; set; }
    public int Status { get; set; }
    public List<Error> Errors { get; set; }
}