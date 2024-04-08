using Kollity.Services.API.Extensions;
using Kollity.Services.API.Helpers;
using Kollity.Services.API.Hubs;
using Kollity.Services.Application;
using Kollity.Services.Infrastructure;
using Kollity.Services.Infrastructure.Implementation.Email;
using Kollity.Services.Persistence;

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

builder.Services.Configure<MailSettings>(builder.Configuration.GetSection(""));
builder.Services
    // .AddFallbackPolicy()
    .AddApplicationConfiguration()
    .AddPersistenceConfigurations(connectionString)
    .AddInfrastructureServices(builder.Configuration)
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