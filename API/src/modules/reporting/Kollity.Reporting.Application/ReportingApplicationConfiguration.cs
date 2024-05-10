global using Kollity.Common.Abstractions.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace Kollity.Reporting.Application;

public static class ReportingApplicationConfiguration
{
    public static IServiceCollection AddReportingApplicationConfiguration(this IServiceCollection services)
    {
        services.AddMediatR(opt =>
        {
            opt.RegisterServicesFromAssembly(typeof(ReportingApplicationConfiguration).Assembly);
        });
        return services;
    }
}