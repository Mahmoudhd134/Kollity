using Kollity.API;
using Kollity.API.Helpers;
using Kollity.Services.API.Extensions;
using Kollity.Services.API.Hubs;
using Kollity.Services.Application;
using Kollity.Services.Infrastructure;
using Kollity.Services.Persistence;
using Kollity.User.API.Extensions;
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


// base service
builder.Services.AddControllers()
    .AddApplicationPart(typeof(KollityUserApiEntryPoint).Assembly)
    .AddApplicationPart(typeof(KollityServicesApiEntryPoint).Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddProblemDetails();
builder.Services.AddHealthChecks();
builder.Services.AddCustomSwaggerGen();
builder.Services.AddModelBindingErrorsMap();
builder.Services.AddMassTransitConfiguration(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddCorsExtension();
builder.Services.AddSignalR();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

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
app.MapServicesHubs();
app.MapHealthChecks("healthy");
app.MapFallbackToFile("index.html");

app.Run();