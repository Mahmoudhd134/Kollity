using Kollity.API.Extensions;
using Kollity.API.Helpers;
using Kollity.API.Hubs;
using Kollity.Application;
using Kollity.Infrastructure;
using Kollity.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCustomSwaggerGen();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddHealthChecks();


<<<<<<< HEAD
var connectionString = builder.Configuration["ConnectionStrings:Default"];
builder.Services
    // .AddFallbackPolicy()
    .AddApplicationConfiguration(builder.Configuration)
    .AddPersistenceConfigurations(connectionString)
    .AddInfrastructureServices()
=======
var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
connectionString = string.IsNullOrWhiteSpace(connectionString)
    ? builder.Configuration["ConnectionStrings:Default"]
    : connectionString;

Console.WriteLine($"Connection String is => {connectionString}");
builder.Services
    // .AddFallbackPolicy()
    .AddApplicationConfiguration()
    .AddPersistenceConfigurations(connectionString)
    .AddInfrastructureServices(builder.Configuration)
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
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
    await app.UpdateDatabase();
}

app.UseHttpsRedirection();

app.UseCors("allowLocalInDevelopment");

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.UseExceptionHandler();

app.MapControllers();
app.MapHubs();
<<<<<<< HEAD
app.MapFallbackToFile("index.html");

=======
app.MapHealthChecks("healthy");
app.MapFallbackToFile("index.html");

await app.UpdateDatabase();
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
app.Run();