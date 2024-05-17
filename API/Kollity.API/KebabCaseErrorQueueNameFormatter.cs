using MassTransit;

namespace Kollity.API;

public class KebabCaseErrorQueueNameFormatter : IErrorQueueNameFormatter
{
    public string FormatErrorQueueName(string queueName)
    {
        return $"{queueName}-error";
    }
}