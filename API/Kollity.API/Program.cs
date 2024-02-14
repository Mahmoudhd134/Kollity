using Kollity.API.Extensions;
using Kollity.API.Helpers;
using Kollity.API.Hubs;
using Kollity.Application;
using Kollity.Infrastructure;
using Kollity.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCustomSwaggerGen();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();


var connectionString = builder.Configuration["ConnectionStrings:Default"];
builder.Services
    // .AddFallbackPolicy()
    .AddApplicationConfiguration(builder.Configuration)
    .AddPersistenceConfigurations(connectionString)
    .AddInfrastructureServices()
    .AddCors()
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
app.MapFallbackToFile("index.html");

app.Run();