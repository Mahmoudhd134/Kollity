using System.Text;
using Kollity.Feedback.Api.Implementation;
using Kollity.Feedback.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Kollity.Feedback.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFeedbackApiServicesInjection(this IServiceCollection services)
    {
        services.AddScoped<IUserServices, HttpUserServices>();
        services.AddHttpContextAccessor();

        return services;
    }
}