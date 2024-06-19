using Kollity.Feedback.Application.Implementation;
using Kollity.Feedback.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Kollity.Feedback.Application;

public static class FeedbackApplicationConfiguration
{
    public static IServiceCollection AddFeedbackApplicationConfiguration(this IServiceCollection services)
    {
        services.AddTransient<IFeedbackServices, FeedbackServices>();
        return services;
    }
}