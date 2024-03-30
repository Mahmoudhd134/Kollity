namespace Kollity.Infrastructure.Abstraction.Events;

public class EmailData
{
    public string ToEmail { get; set; }
    public string ToName { get; set; }
    public string Subject { get; set; }
    public string TextBody { get; set; }
    public string HtmlBody { get; set; }
}