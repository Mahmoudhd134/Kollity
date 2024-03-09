using Kollity.API.Extensions;
using Kollity.API.Helpers;
using Kollity.API.Hubs;
using Kollity.Application;
using Kollity.NotificationServices;
using Kollity.Infrastructure;
using Kollity.NotificationServices.Abstraction;
using Kollity.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCustomSwaggerGen();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddHealthChecks();


var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
connectionString = string.IsNullOrWhiteSpace(connectionString)
    ? builder.Configuration["ConnectionStrings:LocalHost"]
    : connectionString;

Console.WriteLine($"Connection String is => {connectionString}");
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection(""));
builder.Services
    // .AddFallbackPolicy()
    .AddApplicationConfiguration()
    .AddPersistenceConfigurations(connectionString)
    .AddInfrastructureServices()
    .AddNotificationServices(builder.Configuration)
    .AddCorsExtension()
    .AddJwtAuthentication(builder.Configuration)
    .AddClassesConfigurations(builder.Configuration)
    .AddServicesInjection()
    .AddModelBindingErrorsMap()
    .AddSignalR();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseCors("allowLocalInDevelopment");

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.UseExceptionHandler();

app.MapControllers();
app.MapHubs();
app.MapHealthChecks("healthy");
app.MapFallbackToFile("index.html");

await app.UpdateDatabase();
app.Run();