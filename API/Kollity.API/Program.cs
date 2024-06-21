using Kollity.API;
using Kollity.API.Helpers;
using Kollity.Feedback.Api.Extensions;
using Kollity.Feedback.Application;
using Kollity.Feedback.Persistence.Data;
using Kollity.Reporting.Application;
using Kollity.Reporting.Persistence;
using Kollity.Reporting.Persistence.Data;
using Kollity.Services.API.Extensions;
using Kollity.Services.API.Hubs;
using Kollity.Services.Application;
using Kollity.Services.Application.Extensions;
using Kollity.Services.Infrastructure;
using Kollity.Services.Persistence;
using Kollity.Services.Persistence.Data;
using Kollity.User.API.Data;
using Kollity.User.API.Extensions;
using Microsoft.EntityFrameworkCore;
using Kollity.Feedback.Persistence.Extensions;
using KollityServicesApiEntryPoint = Kollity.Services.API.Extensions.ServiceCollectionExtensions;
using KollityUserApiEntryPoint = Kollity.User.API.Extensions.ServiceCollectionExtensions;

var builder = WebApplication.CreateBuilder(args);


// user service
builder.Services.AddUserServicesInjection();
builder.Services.AddUserDatabaseConfig();
builder.Services.AddUserClassesConfigurations(builder.Configuration);

// services service
builder.Services.AddServicesApplicationConfiguration();
builder.Services.AddServicesPersistenceConfiguration();
builder.Services.AddServicesInfrastructureConfiguration();
builder.Services.AddServicesServicesInjection();

// reporting services
builder.Services.AddReportingPersistenceConfiguration();
builder.Services.AddReportingApplicationConfiguration();

// feedback services
builder.Services.AddFeedbackPersistenceConfiguration();
builder.Services.AddFeedbackApplicationConfiguration();
builder.Services.AddFeedbackApiServicesInjection();


// base service
builder.Services.AddControllers()
    .AddApplicationPart(typeof(KollityUserApiEntryPoint).Assembly)
    .AddApplicationPart(typeof(KollityServicesApiEntryPoint).Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddProblemDetails();
builder.Services.AddHealthChecks();
builder.Services.AddCustomSwaggerGen();
builder.Services.AddModelBindingErrorsMap();
var useInMemory = builder.Configuration["Queues:UseInMemory"] == "True";
builder.Services.AddMassTransitConfiguration(builder.Configuration, useInMemory);
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddCorsExtension();
builder.Services.AddSignalR();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

var app = builder.Build();

// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI();
app.UseDeveloperExceptionPage();
// }

app.UseHttpsRedirection();

app.UseCors("allowLocalInDevelopment");

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.UseExceptionHandler();

app.MapControllers();
app.MapServicesHubs();
app.MapHealthChecks("healthy");
app.MapFallbackToFile("index.html");

try
{
    using var scope = app.Services.CreateScope();
    await using var userDbContext = scope.ServiceProvider.GetRequiredService<UserDbContext>();
    await using var serviceDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await using var reportingDbContext = scope.ServiceProvider.GetRequiredService<ReportingDbContext>();
    await using var feedbackDbContext = scope.ServiceProvider.GetRequiredService<FeedbackDbContext>();

    await userDbContext.Database.MigrateAsync();
    await serviceDbContext.Database.MigrateAsync();
    await reportingDbContext.Database.MigrateAsync();
    await feedbackDbContext.Database.MigrateAsync();
}
catch (Exception e)
{
    var logger = app.Services.CreateScope().ServiceProvider.GetRequiredService<ILogger<Program>>();
    logger.LogError(e.GetErrorMessage());
}

app.Run();